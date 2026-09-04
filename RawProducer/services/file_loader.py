from pathlib import Path
from  logging import Logger
import csv
import json

class FileLoader:
    def __init__(self, file_path:Path, logger:Logger):
        self.file_path = file_path
        self.logger = logger

    def load_data(self):
        self.logger.info(f"try to load file : {self.file_path}")
        if not self.file_path.is_file:
            self.logger.error(f"file: {self.file_path} not exists!")
            return []
        
        with open(self.file_path) as file:
            data = csv.DictReader(file)
            for row in data:
                if row:
                    print("before",row)
                    row = json.dumps(row)
                    print("after", row)
                    yield row