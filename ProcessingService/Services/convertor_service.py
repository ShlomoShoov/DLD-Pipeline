import json
from confluent_kafka import Message

def to_dicts(batch:list[Message])-> list[dict]:
    return [json.loads(m.value()) for m in batch]

def to_strs(batch:list[dict]) ->  list[str]:
    return [json.dumps(d) for d in batch]