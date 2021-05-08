namespace DigichList.Application.Helpers
{
    internal static class TelegramBotTextMessages
    {
        internal const string AboutMessage = "DigichList - це Телеграм-бот, що дозволяє публікувати дефекти у номерах готелів.";

        internal const string RegistrationWasSent = "Ви успішно відправили запит на реєстрацію! Вам буде повідомлено про результат якнайшвидше!";

        internal const string DefectSendingForbidden = "На жаль, ви не можете публікувати дефекти.";

        internal const string UserAlreadyExists = "Ви вже надіслали заявку на реєстрацію";

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
    }
}
