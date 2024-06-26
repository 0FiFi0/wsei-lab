﻿using ApplicationCore.Interfaces.Repository;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces;

public class QuizAdminService:IQuizAdminService
{
    private IGenericRepository<Quiz, int> quizRepository;
    private IGenericRepository<QuizItem, int> itemRepository;

    public QuizAdminService(IGenericRepository<Quiz, int> quizRepository, IGenericRepository<QuizItem, int> itemRepository)
    {
        this.quizRepository = quizRepository;
        this.itemRepository = itemRepository;
    }

    public QuizItem AddQuizItem(string question, List<string> incorrectAnswers, string correctAnswer, int points)
    {
        return itemRepository.Add(new QuizItem(question: question, incorrectAnswers: incorrectAnswers, correctAnswer: correctAnswer, id: 0));
    }

    public void UpdateQuizItem(int id, string question, List<string> incorrectAnswers, string correctAnswer, int points)
    {
        var quizItem = new QuizItem(id: id, question: question, incorrectAnswers: incorrectAnswers, correctAnswer: correctAnswer);
        itemRepository.Update(id, quizItem);
    }

    public Quiz AddQuiz(Quiz quiz)
    {
        int id=quizRepository.FindAll().Count+1;
        quiz.Id=id;
        return quizRepository.Add(quiz);
    }
    public QuizItem AddQuizItemToQuiz(int quizId, QuizItem item)
    {
        var quiz = quizRepository.FindById(quizId);
        if (quiz is null)
        {
            throw new Exception();
        }
        var newItem = itemRepository.Add(item);
        quiz.Items.Add(newItem);
        quizRepository.Update(quizId, quiz);
        return newItem;
    }

    public List<QuizItem> FindAllQuizItems()
    {
        return itemRepository.FindAll();
    }

    public List<Quiz> FindAllQuizzes()
    { return quizRepository.FindAll();
    }
}