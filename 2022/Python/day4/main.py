import aoc


def part_one():
    counter = 0
    with open(aoc.get_file_path(__file__)) as f:
        for line in f:
            line = line.rstrip()
            elves = line.split(',')

            e1 = get_section_range(elves[0])
            e2 = get_section_range(elves[1])

            if set((e1)).issubset(e2) or set((e2)).issubset(e1):

                counter += 1

    aoc.print_answer(4, 1, counter)


def part_two():
    counter = 0
    with open(aoc.get_file_path(__file__)) as f:
        for line in f:
            line = line.rstrip()
            elves = line.split(',')

            e1 = get_section_range(elves[0])
            e2 = get_section_range(elves[1])

            if set(e1).intersection(set(e2)):
                counter += 1

    aoc.print_answer(4, 2, counter)


def get_section_range(input):
    i = input.split('-')
    return range(int(i[0]), int(i[1])+1)


def print_range(range):
    r = ""
    for i in range:
        r += str(i)
    print(r)
