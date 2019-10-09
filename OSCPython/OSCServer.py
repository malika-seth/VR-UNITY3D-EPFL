from pyOSC3 import OSCServer
import csv
import datetime
import atexit


cnt = 0
dict_list = []


def handler(addr, tags, data, client_address):
    global cnt
    # global csv_file
    global dict_list
    txt = "OSCMessage '%s' from %s: " % (addr, client_address)
    txt += str(data)
    print(txt, '\n', '   ', data[0])
    dict_list_item = {'count': cnt, 'timestamp': datetime.datetime.now(), 'data_1': data[0], 'data_2': data[1],
                      'data_3': data[2]}
    dict_list.append(dict_list_item)
    # temp = float(data[0])
    # csv_file.write("{0},{1:s},{2},{3},{4}".format(cnt, datetime.datetime.now(), data[0], data[1], data[2]))
    # csv_file.write("{0},{1},{2}\n".format(data[0], data[1], data[2]))
    # csv_file.write("{0},{1},{2}\n".format(float(data[0]), float(data[1]), float(data[2])))
    # csv_file.write("loskjdfkjds")

    # csv_writer.writerow([cnt, datetime.datetime.now(), data[0], data[1], data[2]])
    cnt += 1


def void_handler(addr, tags, data, client_address):
    return


if __name__ == "__main__":
    # s = OSCServer(('192.168.2.17', 9090))  # My Local LAN IP
    s = OSCServer(('127.0.0.1', 9090))  # listen on localhost, port 9090
    s.addMsgHandler('/imu', void_handler)     # call handler() for OSC messages received with the /startup address
    s.addMsgHandler('/inputs/analogue', handler)     # call handler() for OSC messages received with the /startup address

    try:
        s.serve_forever()
    except KeyboardInterrupt:
        print("System Exit Handler")

    with open("output_python.txt", "w") as csv_file:
        for list_item in dict_list:
            csv_file.write('{0},{1},{2},{3},{4}\n'.format(list_item['count'], list_item['timestamp'],
                                                          list_item['data_1'], list_item['data_2'],
                                                          list_item['data_3']))

    exit(0)
