import pygame
import sys
class SilentSound:
    def play(self, *args, **kwargs):
        return None
    def stop(self):
        return None
    def set_volume(self, volume):
        return None
    def fadeout(self, milliseconds):
        return None
    def get_volume(self):
        return 0.0
def load_sound(path):
    if sys.platform == "emscripten":
        return SilentSound()
    try:
        return pygame.mixer.Sound(path)
    except (FileNotFoundError, pygame.error):
        return SilentSound()
