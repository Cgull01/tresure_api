using API_tresure.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace tresure_api.Data.Interfaces
{
    public interface ICardRepository
    {
        Task <ICollection<Card>> GetCards();
        //Task <ICollection<Card>> GetCardsByProjectId(int projectId);


        Task <Card> GetCardById(int id);

        bool CreateCard(Card card);
        bool UpdateCard(Card card);
        bool DeleteCard(Card card);
        bool Save();

    }
}
