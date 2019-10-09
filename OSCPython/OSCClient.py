import pyOSC3 as OSC
import time

c = OSC.OSCClient()
c.connect(('192.168.2.17', 9090))   # localhost, port 57120
# c.connect(('192.168.95.1', 7070))   # localhost, port 57120
# c.connect(('169.254.52.176', 6969))
# c.connect(('127.0.0.1', 6969))   # localhost, port 57120
oscmsg = OSC.OSCMessage()
cnt = 0
delta = .025
oscmsg.setAddress("/inputs/analogue")

while True:
    oscmsg.append(cnt)
    oscmsg.append(cnt)
    oscmsg.append(cnt)
    c.send(oscmsg)
    oscmsg.clearData()
    if cnt > 6 or cnt < 0:
        delta = delta * -1
    cnt += delta
    print(cnt)
    time.sleep(.05)
