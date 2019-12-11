using System;
using Xunit;
 /// <summary> 
    /// В системе представлен список блоков с предопределенным содержимым, которыми авторизованный пользователь может
    /// оперировать в процессе настройки главной страницы портала. Один из блоков - «Быстрые ссылки» - список ссылок, определяемых пользователем.
    /// Класс содержит методы для работы с пользовательскими ссылками.
    /// </summary>
    public class UserLinkService
    {
        private IUnitOfWork unitOfWork = DI.Resolve<IUnitOfWork>();

        /// <summary>
        /// Возвращает все «Быстрые ссылки» пользователя(Id, URI, название, вес).
        /// </summary>
        /// <returns>Все ссылки пользователя.</returns>
        public virtual List<UserLinkViewModel> List()
        {
            // Получение Id текущего пользователя. Если не авторизован, то вернется 0. Авторизация проверяется в классе контроллера.
            var userId = UserUtils.CurrentUserId;

            // Получение всех ссылок пользователя из репозитория и преобразование к списку.
            var list = unitOfWork.UserLinkRepository.FilterByExpression(l => l.UserId == userId)
                .AsEnumerable()
                .OrderBy(p=>p.Weight)
                .Select(p => (UserLinkViewModel)p)
                .ToList();

            return list;
        }

        public bool GuestLinkAccess()
        {
            var linkBlock = unitOfWork.BlockRepository.First(p => p.Method == "RenderLink");
            if (linkBlock == null || !linkBlock.ShowToGuest)
                return false;
            return true;
        }

        /// <summary>
        /// Возвращает пользовательскую ссылку по идентификатору ссылки (Id, URI, название, вес).
        /// </summary>
        /// <param name="id">Идентификатор пользовательской ссылки.</param>
        /// <returns>Пользовательскую ссылку.</returns>
        public virtual UserLinkViewModel BuildViewModel(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Неверный идентификатор ссылки!".Lang());
            else
            {
                try
                {
                    var domainLink = unitOfWork.UserLinkRepository.Find(id);

                    if (domainLink != null)
                        return (UserLinkViewModel)domainLink;

                    return null;
                }
                catch (Exception e)
                {
                    // Запись ошибки в лог.
                    Logger.WriteLogError(e);

                    return null;
                }
            }
        }

        /// <summary>
        /// Создает пользовательскую ссылку.
        /// </summary>
        /// <param name="userLink">Объект модели представления с данными о пользовательской ссылке.</param>
        /// <returns>Новая пользовательская ссылка.</returns>
        public virtual UserLinkViewModel Create(UserLinkViewModel userLink)
        {
            if (userLink == null)
                throw new ArgumentNullException("Невозможно создать пустую ссылку!".Lang());
            try
            {
                // Создается новый объект доменной модели данных.
                var domainLink = (UserLink)userLink;

                // Добавление нового объекта.
                unitOfWork.UserLinkRepository.Add(domainLink);
                domainLink.UserId = UserUtils.CurrentUserId;

                unitOfWork.Save();

                // Вызываем событие создания новой группы
                EventSystem.FireTypedEvent(userLink, PossibleEventAction.Create);

                return (UserLinkViewModel)domainLink;
            }
            catch (Exception e)
            {
                // Запись ошибки в лог.
                Logger.WriteLogError(e);

                return null;
            }
        }
    }
