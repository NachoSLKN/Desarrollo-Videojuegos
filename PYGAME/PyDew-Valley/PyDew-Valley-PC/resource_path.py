import os
import sys


def resource_path(relative_path):
    """Devuelve una ruta válida tanto en Python como en un .exe de PyInstaller."""
    if getattr(sys, "frozen", False) and hasattr(sys, "_MEIPASS"):
        base_path = sys._MEIPASS
    else:
        base_path = os.path.abspath(".")

    return os.path.join(base_path, relative_path)
