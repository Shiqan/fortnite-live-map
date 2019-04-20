import numpy as np
import imutils
import cv2
import os

MINIMAP_SCALE = 2.2


def classify(img):
    world = cv2.imread("world.jpg")
    minimap_org = cv2.imread(img)
    (minimapHeight, minimapWidth) = minimap_org.shape[:2]
    minimapHeight = int(minimapHeight / MINIMAP_SCALE)
    minimapWidth = int(minimapWidth / MINIMAP_SCALE)
    minimap = cv2.resize(minimap_org, (minimapHeight, minimapWidth))

    # Template matching
    result = cv2.matchTemplate(world, minimap, cv2.TM_CCOEFF)
    (_, _, minLoc, maxLoc) = cv2.minMaxLoc(result)

    # Extract matched part
    leftx, lefty = maxLoc
    rightx, righty = (leftx + minimapWidth, lefty + minimapHeight)
    matchedPart = world[lefty:righty, leftx:rightx]

    # Apply mask to world map but reset the matched part
    mask = np.zeros(world.shape, dtype="uint8") 
    world = cv2.addWeighted(world, 0.25, mask, 0.75, 0)
    world[lefty:righty, leftx:rightx] = matchedPart

    cv2.imshow("Fortnite World", imutils.resize(world, height=650))
    cv2.waitKey(0)

if __name__ == '__main__':
    # classify('minimap_org.jpg')
    files = [f'./screenshots/{f}' for f in os.listdir('./screenshots') if f.endswith('.png')]
    for f in files:
        classify(f)  