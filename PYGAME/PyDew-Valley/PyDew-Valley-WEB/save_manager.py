import json
import os


class SaveManager:

    SAVE_PATH = "savegame.json"

    @classmethod
    def save_exists(cls):
        return os.path.exists(cls.SAVE_PATH)

    @classmethod
    def save_game(cls, data):
        with open(cls.SAVE_PATH, "w", encoding="utf-8") as file:
            json.dump(data, file, indent=4)

    @classmethod
    def load_game(cls):
        if not cls.save_exists():
            return None

        with open(cls.SAVE_PATH, "r", encoding="utf-8") as file:
            return json.load(file)