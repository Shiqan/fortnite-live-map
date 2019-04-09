from multiprocessing import Process, Queue
import time
import uuid
import mss
from mss.tools import to_png


def grab(queue):
    with mss.mss() as sct:
        monitor_number = 1
        mon = sct.monitors[monitor_number]

        # The screen part to capture
        monitor = {
            "top": mon["top"] + 18,
            "left": mon["width"] - 375 - 18,
            "width": 375,
            "height": 375,
            "mon": monitor_number,
        }

        for _ in range(60):
            queue.put(sct.grab(monitor))
            time.sleep(10)

    # Tell the other worker to stop
    queue.put(None)


def save(queue):
    output = "./screenshots/{}.png"

    while "there are screenshots":
        img = queue.get()
        if img is None:
            break

        to_png(img.rgb, img.size, output=output.format(uuid.uuid4()))


if __name__ == "__main__":
    queue = Queue()

    # 2 processes: one for grabing and one for saving PNG files
    Process(target=grab, args=(queue,)).start()
    Process(target=save, args=(queue,)).start()
