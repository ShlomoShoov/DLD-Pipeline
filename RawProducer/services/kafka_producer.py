from confluent_kafka import Producer
from models.kafka_config import KafkaConfig

class KafkaProducer:
    def __init__(self, configs:KafkaConfig):
        self.configs = configs
        self.producer:Producer = Producer(configs.get_producer_configs())

    def produce(self, value:str):
        self.producer.produce(self.configs.raw_topic_name, value=value)
    

    def dispose(self):
        self.producer.flush()
        