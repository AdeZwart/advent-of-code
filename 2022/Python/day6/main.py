import aoc


def part_one():
    with open(aoc.get_file_path(__file__)) as f:
        for line in f:
            line = line.rstrip()

        marker_position = find_marker(line, 4)
        aoc.print_answer(6, 1, marker_position)


def part_two():
    with open(aoc.get_file_path(__file__)) as f:
        for line in f:
            line = line.rstrip()

        marker_position = find_marker(line, 14)
        aoc.print_answer(6, 2, marker_position)


def find_marker(message, length):
    for i in range(0, len(message)-length):
        if (len(set(message[i:i+length])) == length):
            return i+length
