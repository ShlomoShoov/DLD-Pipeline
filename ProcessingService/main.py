import os
from Models.kafka_configs import KafkaConfigs
from Services.producer_service import KafkaProducer
from Services.logger import setup_global_logger
from Orchestrators.process_service_orchestrator import Orchestrator
from Services.consumer_service import ConsumerService

bootstrap_server = os.environ.get("bootstrap_server", default="localhost:9092")
raw_topic_name =  os.environ.get("raw_topic_name", default="raw_data")
client_id = os.environ.get("client_id",default="raw+producer")
processing_topic_name = os.environ.get("processing_topic_name", default="processing_data")
group_id = os.environ.get("group_id", default="processing_service")

logger = setup_global_logger()

kafka_config = KafkaConfigs(bootstrap_server=bootstrap_server, raw_topic_name= raw_topic_name, client_id= client_id,
                            processing_topic_name=processing_topic_name, group_id=group_id)

producer = KafkaProducer(configs=kafka_config)
consumer = ConsumerService(configs=kafka_config, logger=logger)

orchestrator = Orchestrator(producer=producer, consumer=consumer, logger=logger)

orchestrator.run()