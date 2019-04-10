import os

import cv2
import imutils
import numpy as np

from dataclasses import dataclass, field

MINIMAP_SCALE = 2.2

@dataclass
class Coord:
    x: int
    y: int
    timestamp: int = 0


def find_location(img, debug=False):
    world = cv2.imread("world.jpg")
    minimap_org = cv2.imread(img)
    (minimap_height, minimap_width) = minimap_org.shape[:2]
    minimap_height = int(minimap_height / MINIMAP_SCALE)
    minimap_width = int(minimap_width / MINIMAP_SCALE)
    minimap = cv2.resize(minimap_org, (minimap_height, minimap_width))

    # Template matching
    result = cv2.matchTemplate(world, minimap, cv2.TM_CCOEFF_NORMED)

    threshold = 0.8
    loc = np.where( result >= threshold)

    if loc[0].size == 0:
        return "no matching location found..."

    (_, _, minLoc, maxLoc) = cv2.minMaxLoc(result)

    # Extract matched part
    leftx, lefty = maxLoc

    if debug:
        rightx, righty = (leftx + minimap_width, lefty + minimap_height)
        matched_part = world[lefty:righty, leftx:rightx]

        # Apply mask to world map but reset the matched part
        mask = np.zeros(world.shape, dtype="uint8") 
        world = cv2.addWeighted(world, 0.25, mask, 0.75, 0)
        world[lefty:righty, leftx:rightx] = matched_part

        cv2.imshow("Fortnite World", imutils.resize(world, height=650))
        cv2.waitKey(0)
    
    return Coord(leftx + (minimap_width / 2), lefty + (minimap_height / 2))

if __name__ == '__main__':
    find_location('./screenshots/fd4b8d7c-df03-4a9c-8474-e77d7578a67b.png', debug=True)
    # find_location('./screenshots/00b339f5-6e3c-4ce8-8f74-7a88dc38be84.png', debug=True)
    files = [f'./screenshots/{f}' for f in os.listdir('./screenshots') if f.endswith('.png')]
    for f in files:
        print(find_location(f))
