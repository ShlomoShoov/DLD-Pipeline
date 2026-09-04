import pandas as pd

def cleaning(df:pd.DataFrame)-> None:
    df.AILearnHow = df.AILearnHow.str.split(";")
    df.LearnCode = df.LearnCode.str.split(";")
    df.YearsCode = df.YearsCode.astype('Int64')
    

def label_experience_level(years:int):
    if years > 11:
        return "Highly Experienced"
    if years > 6:
        return "Experienced"
    if years > 3:
        return "Early Career"
    if years > 0:
        return "Beginner"
    return "Unknown"


def is_using_documentation(learn_code:list[str])-> bool:
    if not isinstance(learn_code, list):
        return False
    for learn_way in learn_code:
        if "Technical documentation" in learn_way:
            return True
    return False

def is_using_ai(learn_code:list[str]) -> bool:
    if not isinstance(learn_code, list):
        return False
    for learn_way in learn_code:
        if "AI CodeGen tools" in learn_way:
            return True
        if "AI-enabled apps" in learn_way:
            return True
    return False

def is_using_stack(learn_code:str)-> bool:
    if not isinstance(learn_code, list):
        return False
    for learn_way in learn_code:
        if "Stack Overflow" in learn_way:
            return True
    return False



def processing(df:pd.DataFrame) -> None:
    df["experienceLevel"] = df.YearsCode.apply(label_experience_level)
    df["usesDocumentation"] = df.LearnCode.apply(is_using_documentation)
    df["usesStackOverflow"] = df.LearnCode.apply(is_using_stack)
    df["usesStackOverflow"] = df.LearnCode.apply(is_using_stack)



