from confluent_kafka import Consumer, Message
from Models.kafka_configs import KafkaConfigs
from datetime import timedelta
import time
from logging import Logger

class ConsumerService:
    def __init__(self, configs:KafkaConfigs, logger:Logger):
        self.configs = configs
        self.consumer:Consumer = Consumer(configs.get_consumer_configs(auto_commit=False, earliest_reset=True))
        self.consumer.subscribe([self.configs.raw_topic_name])
        self.logger = logger
    def consume(self, batch_size:int = 100, max_wait_time:timedelta = timedelta(seconds=10))-> list[Message]:
        batch = []
        timeout_second = max_wait_time.total_seconds()
        deadline = time.monotonic() + timeout_second

        while len(batch) < batch_size:
            remaining_time = deadline - time.monotonic()

            if remaining_time <= 0:
                break

            msg:Message = self.consumer.poll(timeout=remaining_time)


            if msg == None or  not msg.value() :
                continue

            if msg.error():
                self.logger.error(f"Kafka event error: {msg.error()}")
                continue

            batch.append(msg)

        return batch

    def commit(self, msgs_to_commit:list[Message]):
        for msg in msgs_to_commit:
            self.consumer.commit(msg)

    def dispose(self):
        self.consumer.unsubscribe()
        self.consumer.close()

        
