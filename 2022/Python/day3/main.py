import aoc


def part_one():
    priority_sum = 0
    with open(aoc.get_file_path(__file__)) as f:
        for line in f:
            line = line.rstrip()

            compartments = get_compartment_content(line)

            common_item = list(set(compartments[0]) & set(compartments[1]))[0]
            priority_sum += calculate_priority(common_item)

        aoc.print_answer(1, priority_sum)


def part_two():
    priority_sum = 0
    with open(aoc.get_file_path(__file__)) as f:
        content = f.read().splitlines()

        for i in range(0, len(content), 3):
            group = content[i:i+3]

            common_item = list(set(group[0]) & set(group[1]))
            common_item = list(set(common_item) & set(group[2]))[0]

            priority_sum += calculate_priority(common_item)

    aoc.print_answer(2, priority_sum)


def calculate_priority(input):
    if (input.isupper()):
        reduction = 38
    else:
        reduction = 96

    return ord(input) - reduction


def get_compartment_content(input):
    content = []
    content_length = int(len(input) / 2)
    # get first compartment content
    content.append(input[0:content_length])
    # get second compartment content
    content.append(input[(content_length):])

    return content
