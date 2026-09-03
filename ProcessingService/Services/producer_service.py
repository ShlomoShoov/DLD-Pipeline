from confluent_kafka import Producer
from Models.kafka_configs import KafkaConfigs

class KafkaProducer:
    def __init__(self, configs:KafkaConfigs):
        self.configs = configs
        self.producer:Producer = Producer(configs.get_producer_configs())

    def produce(self, batch:list[str]):
        for value in batch:
            self.producer.produce(self.configs.processing_topic_name, value=value)
        self.producer.flush()
    


        