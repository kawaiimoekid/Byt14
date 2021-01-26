# Byt14 s15383

Zadanie nr. 14 z przedmiotu BYT by Jakub Kurchan s15383.

Jest to aplikacja konsolowa napisana w .NET Core 3.1 imitująca sklep w którym można rezerwować oraz kupować artykuły (Degree).

# Użyte wzorce
## Stan

Zdefiniowany jest interfejs IAvailabilityState określający dostępność produktu z 3 konkretnymi implementacjami AvailableAvailabilityState, ReservedAvailabilityState, SoldAvailabilityState. Interfejs posiada metody zwracające nazwę stanu, flagę czy stan pozwala na zakup produktu oraz kolor do wyświetlenia w konsoli.

## Memento

W klasie Degree jest zdefiniowana podklasa DegreeSnapshot przechowująca odwołanie do obiektu klasy Degree oraz poprzedni stan produktu. Klasa Degree posiada metodę CreateSnapshot tworzącą obiekt klasy DegreeSnapshot. Klasa DegreeSnapshot posiada metodę Restore przywracającą obiekt klasy Degree do poprzedniego stanu. Z poziomu interfejsu zarezerwowanie produktu bądź zakup tworzy DegreeSnapshot i odkłada go na stos. Następnie inna opcja pozwala ściągać snapshoty ze stosu i przywracać poprzednie stany obiektów.

## Builder

Zdefiniowana jest klasa ShopBuilder posiadająca metody: SetName ustawiająca nazwę sklepu, AddDegree dodająca artykuł do sklepu, AllowReservation określająca, czy sklep pozwala na rezerwacje oraz Build zwracająca utworzony obiekt klasy Shop.

Do tego zdefiniowana jest klasa ShopDirector posiadająca metody BuildDemoShop oraz BuildProdShop, które zwracają obiekty klasy Shop utworzone za pomocą ShopBuilder'a. ShopDirector wykorzystywany jest przy uruchomieniu programu w pliku Program.cs.
