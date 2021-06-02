namespace DigichList.Application.Helpers
{
    public static class TelegramBotTextMessages
    {
        internal const string AboutMessage = "DigichList - це Телеграм-бот, що дозволяє публікувати дефекти у номерах готелів.";

        internal const string RegistrationWasSent = "Ви успішно відправили запит на реєстрацію! Вам буде повідомлено про результат якнайшвидше!";

        internal const string DefectSendingForbidden = "На жаль, ви не можете публікувати дефекти.";

        internal const string DefectWasSent = "Дефект був успішно надісланий на подальшу обробку.";

        internal const string DefectWasNotFound = "На жаль, не вдалося знайти дефект.";

        internal const string StatusWasSuccessfullyChanged = "Статус дефекту був успішно змінений";

        internal const string UserAlreadyExists = "Ви вже надіслали заявку на реєстрацію";

        internal const string UserAlreadyRegistered = "Ви вже зареєстровані";

        internal const string UserDidNotApplyForRegistration = "Ви не можете публікувати дефект, так як ви не подавали заявку на реєстрацію.\n" +
                                                                "Введіть /registerme, щоб подати заявку на реєстрацію.";

        internal const string OnlyTextAllowed = "Будь ласка, введіть дані в текстовому форматі";

        internal const string InvalidStatus = "На жаль, такого статусу не існує. Виберіть існуючий статус з клавіатури";

        internal const string HowItWorks = "Принцип роботи бота наступний:\n" +
                                            "1. Ви подаєте заявку на реєстрацію\n" +
                                            "2. Адмін підтверджує або не підтверджує її\n" +
                                            "3. Якщо адмін підтвердив заявку - ви маєте дозвіл на публікацію дефектів " +
                                            "(а також отимувати дефекти на виконання, якщо вам буде призначена роль виконавця)";

        internal const string WelcomeMessageText = "Команди:\n" +
                                        "/howitworks      - принцип роботи бота\n" +
                                        "/registerme      - подати заявку на реєстрацію\n" +
                                        "/newdefect       - надіслати виявлений дефект\n" +
                                        "/setdefectstatus - керувати статусом дефекту\n" +
                                        "/about           - про бота";

        public const string AskForRoomNumberMessage = "Введіть номер в готелі, в якому був виявлений дефект";

        public const string AskForDefectDescriptionMessage = "Опишіть дефект";

        public const string OnlyNumbersAllowedWarning = "Потрібно ввести чисельне значення";

        public const string UserWasNotFound = "На жаль, неможливо надіслати дефект, так як ви не пройшли реєстрацію.";
    }
}
