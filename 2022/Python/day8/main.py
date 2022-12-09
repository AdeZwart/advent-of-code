import aoc


def part_one():
    forrest = []
    visibility = 0
    with open(aoc.get_file_path(__file__)) as f:
        for line in f:
            # create the forrest grid
            forrest.append(list(line.rstrip()))

        # The outer trees are always visible
        visibility += len(forrest) * 2
        visibility += len(forrest[0]) * 2
        # corners are counted twice, so subtract those
        visibility -= 4

        for i in range(1, len(forrest)-1):
            for j in range(1, len(forrest[i])-1):
                # If the tree is visible from any direction
                if (is_visible_horizontal(forrest, i, j) or is_visible_vertical(forrest, i, j)):
                    # Increment the counter
                    visibility += 1

        aoc.print_answer(8, 1, visibility)


def part_two():
    forrest = []
    top_score = 0
    with open(aoc.get_file_path(__file__)) as f:
        for line in f:
            # create the forrest grid
            forrest.append(list(line.rstrip()))

        # The outer edges score 0 in at least one direction
        # This will always result in a scenic score of 0

        for i in range(1, len(forrest)-1):
            for j in range(1, len(forrest[i])-1):
                score = calculate_scenic_score(forrest, i, j)

                if (score > top_score):
                    top_score = score

        aoc.print_answer(8, 2, top_score)


def calculate_scenic_score(forrest: list, tree_y: int, tree_x: int):
    left_score = 0
    right_score = 0
    up_score = 0
    down_score = 0

    tree = forrest[tree_y][tree_x]
    forrest_width = len(forrest[0])
    forrest_height = len(forrest)

    # check left
    for i in range(tree_x-1, -1, -1):
        left_score += 1
        if (forrest[tree_y][i] >= tree):
            break

    # check right
    for i in range(tree_x+1, forrest_width):
        right_score += 1
        if (forrest[tree_y][i] >= tree):
            break

    # check up
    for i in range(tree_y-1, -1, -1):
        up_score += 1
        if (forrest[i][tree_x] >= tree):
            break

    # check down
    for i in range(tree_y+1, forrest_height):
        down_score += 1
        if (forrest[i][tree_x] >= tree):
            break

    return left_score * right_score * up_score * down_score


def is_visible_horizontal(forrest: list, tree_y: int, tree_x: int):
    forrest_width = len(forrest[0])
    if (tree_x > forrest_width / 2):
        # check right
        return is_visible_right(forrest, tree_y, tree_x) or is_visible_left(forrest, tree_y, tree_x)
    else:
        return is_visible_left(forrest, tree_y, tree_x) or is_visible_right(forrest, tree_y, tree_x)


def is_visible_vertical(forrest: list, tree_y: int, tree_x: int):
    forrest_height = len(forrest)
    if (tree_y > forrest_height / 2):
        return is_visible_down(forrest, tree_y, tree_x) or is_visible_up(forrest, tree_y, tree_x)
    else:
        return is_visible_up(forrest, tree_y, tree_x) or is_visible_down(forrest, tree_y, tree_x)


def is_visible_right(forrest: list, tree_y: int, tree_x: int):
    forrest_width = len(forrest[0])
    tree = forrest[tree_y][tree_x]
    # check right
    for i in range(tree_x+1, forrest_width):
        if (forrest[tree_y][i] >= tree):
            # It's not visible from the right
            return False
    return True


def is_visible_left(forrest: list, tree_y: int, tree_x: int):
    tree = forrest[tree_y][tree_x]
    for i in range(tree_x-1, -1, -1):
        if (forrest[tree_y][i] >= tree):
            # It's not visible from the left
            return False
    return True


def is_visible_down(forrest: list, tree_y: int, tree_x: int):
    forrest_height = len(forrest)
    tree = forrest[tree_y][tree_x]
    for i in range(tree_y+1, forrest_height):
        if (forrest[i][tree_x] >= tree):
            # It's not visible from the bottom
            return False
    return True


def is_visible_up(forrest: list, tree_y: int, tree_x: int):
    tree = forrest[tree_y][tree_x]
    for i in range(tree_y-1, -1, -1):
        if (forrest[i][tree_x] >= tree):
            # It's not visible from the top
            return False
    return True
