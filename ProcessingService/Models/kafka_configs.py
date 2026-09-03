class KafkaConfigs:
    def __init__(self, bootstrap_server:str, raw_topic_name:str, client_id:str, processing_topic_name:str, group_id:str):
        self.bootstrap_server = bootstrap_server
        self.raw_topic_name = raw_topic_name
        self.client_id = client_id
        self.processing_topic_name = processing_topic_name
        self.group_id = group_id

    def get_producer_configs(self)->dict:
        return {'bootstrap.servers':self.bootstrap_server,
                 'client.id': self.client_id}

    def get_consumer_configs(self, auto_commit:bool=True, earliest_reset:bool=True)->dict:
        return {'bootstrap.servers': self.bootstrap_server,
                'group.id': self.group_id,
                'enable.auto.commit': 'true' if auto_commit else 'false' ,
                'auto.offset.reset': 'earliest' if earliest_reset else 'smallest'}
