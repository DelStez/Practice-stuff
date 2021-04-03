# Составить модель и программу,
# которая при помощи правила орфографии
# выводила правильно написанное слово.
# Если надо вставить букву в слово, то она заменяется *.
# В программе проверить ввод слова
# (только русские буквы и *, причем одна).
# Также проверить, что введенное слово подходит под данное правило.
# Правило написания букв О-А в безударных корнях -лаг-, -лож-.
# Например, положить, предложить, полагать, полог.

# Подлкючение регулярных выражений
import re


# Проверка алфавита
def check_alphabet(n):
    match = re.match("""^[а-яА-ЯёЁ][а-яё *]+$""", n)
    return bool(match)


# Проверка длины
def check_length(n):
    if (n != 0) & (n > 2):
        return True
    else:
        print("Ошибка длины слова: длина входного слова меньше или равен длине корня")
        return False


# правило -лаг -лож
def rule_lag_loj(word1):
    #  Есть корень лож
    if bool(re.search(r'л\*ж', word1)):
        word1 = re.sub(r'л\*ж', 'лож', word1)
    # Слова исключения полог слогировать
    elif bool(re.search(r'[Cс]л\*г\b', word1)) | bool(re.search(r'[Пп]ол\*г\b', word1)):
        word1 = re.sub(r'л\*г', 'лог', word1)
    else:
        if not bool(re.search(r'л\*г', word1)):
            print("Нет корней -лаг -лож -лож")
            return
        # есть суффикс -а
        elif bool(re.search(r'л\*г[^а]', word1)) | bool(re.search(r'л\*г\b', word1)):
            word1 = re.sub(r'л\*г', 'лог', word1)
        # нет суффикса
        else:
            word1 = re.sub(r'л\*г', 'лаг', word1)
    print(word1)
    return


# Общая проверка
def check_entered_word(current):
    if not check_alphabet(current):
        print("Нарушение требования к программе:  \"только русские буквы\"")
        return
    if word.count("*") <= 1:
        if check_length(len(current)):
            rule_lag_loj(current)
    else:
        print("Нарушение требования к программе:  \"*, причем одна\"")


word = input("Введите слово X cо *: ")
check_entered_word(word)
