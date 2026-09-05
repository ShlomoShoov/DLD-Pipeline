class KafkaConfig:
    def __init__(self, bootstrap_server:str, raw_topic_name:str, client_id:str ):
        self.bootstrap_server = bootstrap_server
        self.raw_topic_name = raw_topic_name
        self.client_id = client_id

    def get_producer_configs(self)->dict:
        return {'bootstrap.servers':self.bootstrap_server,
                 'client.id': self.client_id}