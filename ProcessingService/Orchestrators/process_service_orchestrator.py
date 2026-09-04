from Services.consumer_service import ConsumerService
import Services.convertor_service  as convertor
import  Services.processor_service as processor
from Services.producer_service import KafkaProducer
from logging import Logger
from confluent_kafka import Message

class Orchestrator:
    def __init__(self,consumer:ConsumerService, producer:KafkaProducer, logger:Logger):
        self.consumer = consumer
        self.producer = producer
        self.logger = logger

    def run(self):
        self.logger.info("Starting the server")
        batch_number:int = 0
        while True:
            self.logger.info(f"start new batch: {batch_number}")
            
            batch:list[Message] = self.consumer.consume()
            self.logger.info(f"Got {len(batch)} msg from consumer. offsets, topic: {[(msg.offset(),  msg.topic()) for msg in batch]}")

            processed_batch:list[dict] = processor.process(convertor.to_dicts(batch))
            self.logger.info(f"process the batch, sample result: {processed_batch[0] if len(processed_batch)>0 else "batch empty"}")

            self.producer.produce(convertor.to_strs(processed_batch))
            self.consumer.commit(batch)

            self.logger.info(f"send the batch {batch_number} to kafka")
            batch_number += 1
