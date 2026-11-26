Rest Api do zarządzania katalogiem produktów
Wymagania:
.Net SDK 9+

Endpointy:
[GET] /api/product : lista wszystkich produktów
[GET] /api/product?name= : pobiera produkt po nazwie
[POST] /api/product : dodaje nowy obiekt [Produkt]

Data flow:
Na zewnątrz wypuszczane są obiekty DTO, aby klient nie znał modelu domenowego.
W serwisie [ProductManagementService] znajduje się logika biznesowa aplikacji.
Tam też następuje konwersja typu z domenowego Models.Product na DTO.Product i przetworzone dane przekazywane są do kontrolera.

Zabezpieczenie api:
W api istnieje zabezpieczenie DTO pod kątem wymagania pól, długości znaków, oraz walidacja wartości ujemnych.
Serwis dodatkowo sprawdza poprawność logiki biznesowej w przypadku ominięcia warstwy DTO.
Podpięty jest GlobalExceptionHandler implementujący IExceptionHandler i jest zarejestrowany przez AddExceptionHandler, włączony w potoku żądań app.UseExceptionHandler() do logowania obsługi błędów.
Błędy wypisywane są w konsoli aplikacji. Nieobsłużone wyjątki są logowane przez ILogger<GlobalExceptionHandler>

Testy jednostkowe:
Przypadki testowe zostały stworzone z udziałem sztucznej inteligencji Claude AI.
Testy logiki serwisu znajdują się w ProductManagementServiceTests (testowanie m.in. dodawanie produktów, przycinanie spacji, generowanie Guid).
