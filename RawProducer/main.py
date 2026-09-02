import os
from models.kafka_config import KafkaConfig
from services.kafka_producer import KafkaProducer
from services.file_loader import FileLoader
from services.logger import setup_global_logger
from orchestrators.raw_producer_orchestrator import RawProducerOrchestrator
from pathlib import Path

bootstrap_server = os.environ.get("bootstrap_server", default="localhost:9092")
raw_topic_name =  os.environ.get("raw_topic_name", default="raw_data")
client_id = os.environ.get("client_id",default="raw+producer")
data_path = Path("data", "developer_ai_learning_raw.csv")

logger = setup_global_logger()

kafka_config = KafkaConfig(bootstrap_server=bootstrap_server, raw_topic_name= raw_topic_name, client_id= client_id)

producer = KafkaProducer(configs=kafka_config)
file_loader = FileLoader(file_path=data_path, logger=logger)

orchestrator = RawProducerOrchestrator(kafka_producer=producer, file_loader=file_loader, logger=logger)

orchestrator.run()