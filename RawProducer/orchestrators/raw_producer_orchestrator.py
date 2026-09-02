from services.file_loader import FileLoader
from services.kafka_producer import KafkaProducer
from logging import Logger

class RawProducerOrchestrator:
    def __init__(self, kafka_producer:KafkaProducer, file_loader:FileLoader, logger:Logger):
        self.kafka_producer = kafka_producer
        self.file_loader = file_loader
        self.logger = logger

    def run(self):
        rows = self.file_loader.load_data()
        for row in rows:
            self.kafka_producer.produce(row)
            self.logger.info(f"produce data to kafka: {row}")
        self.kafka_producer.dispose()