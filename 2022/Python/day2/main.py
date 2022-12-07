import aoc

LOSE = 0
DRAW = 3
WIN = 6


def part_one():
    total_score = 0
    with open(aoc.get_file_path(__file__)) as f:
        for line in f:
            total_score += rock_paper_scissors(line)

        aoc.print_answer(2, 1, total_score)


def part_two():
    total_score = 0
    with open(aoc.get_file_path(__file__)) as f:
        for line in f:
            total_score += rock_paper_scissors_2(line)

        aoc.print_answer(2, 2, total_score)


def rock_paper_scissors(input):
    """
    Scissors < Rock
    Paper < Scissors
    Rock < Paper
    """
    ROCK = ["A", "X"]
    PAPER = ["B", "Y"]
    SCISSORS = ["C", "Z"]

    score = 0
    game = input.split()

    # Add score for chosen attribute
    score += get_chosen_attribute_score(game[-1])

    # draw
    if game[0] in ROCK and game[-1] in ROCK:
        score += DRAW

    if game[0] in PAPER and game[-1] in PAPER:
        score += DRAW

    if game[0] in SCISSORS and game[-1] in SCISSORS:
        score += DRAW

    # scissors vs paper, WIN!
    if (game[0] in SCISSORS and game[-1] in ROCK):
        score += WIN

    # paper vs rock, WIN!
    if (game[0] in PAPER and game[-1] in SCISSORS):
        score += WIN

    # rock vs scissors, WIN!
    if (game[0] in ROCK and game[-1] in PAPER):
        score += WIN

    return score


def rock_paper_scissors_2(input):
    ROCK = "A"
    PAPER = "B"
    SCISSORS = "C"

    score = 0
    game = input.split()

    if (game[-1] == "X"):
        score += LOSE
        if (game[0] == ROCK):
            score += get_chosen_attribute_score(SCISSORS)
        if (game[0] == PAPER):
            score += get_chosen_attribute_score(ROCK)
        if (game[0] == SCISSORS):
            score += get_chosen_attribute_score(PAPER)

    if (game[-1] == "Y"):
        score += DRAW
        score += get_chosen_attribute_score(game[0])

    if (game[-1] == "Z"):
        score += WIN
        if (game[0] == ROCK):
            score += get_chosen_attribute_score(PAPER)
        if (game[0] == PAPER):
            score += get_chosen_attribute_score(SCISSORS)
        if (game[0] == SCISSORS):
            score += get_chosen_attribute_score(ROCK)

    return score


def get_chosen_attribute_score(input):
    score = 0
    match input:
        case "A" | "X":
            score += 1
        case "B" | "Y":
            score += 2
        case "C" | "Z":
            score += 3

    return score
