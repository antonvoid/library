\! chcp 1251

DROP DATABASE IF EXISTS kp;

CREATE DATABASE kp;

\connect kp

CREATE TABLE series (
    series_id SERIAL PRIMARY KEY,
    title VARCHAR(100),
    number_of_books INT
);

CREATE TABLE country(
    country_id SERIAL PRIMARY KEY,
    country_name VARCHAR(100)
);

CREATE TABLE author(
    author_id SERIAL PRIMARY KEY,
    last_name VARCHAR(200),
    first_name VARCHAR(200),
    middle_name VARCHAR(200),
    country VARCHAR(100),
    date_of_birth DATE,
    date_of_death VARCHAR(20)
);

CREATE TABLE place(
    place_id SERIAL PRIMARY KEY,
    room INT,
    line INT,
    shelf INT,
    position INT
);

CREATE TABLE visitor(
    visitor_id SERIAL PRIMARY KEY,
    issuance_id SERIAL,
    last_name VARCHAR(200),
    first_name VARCHAR(200),
    middle_name VARCHAR(200),
    addres VARCHAR(200),
    visitor_phone_number VARCHAR(20)
);

CREATE TABLE employee(
    employee_id SERIAL PRIMARY KEY,
    last_name VARCHAR(200),
    first_name VARCHAR(200),
    middle_name VARCHAR(200),
    employee_phone_number VARCHAR(20),
    position VARCHAR(50)
);

CREATE TABLE issuance(
    issuance_id SERIAL PRIMARY KEY,
    visitor_id INT,
    date_of_issuance DATE,
    date_of_return DATE,
    FOREIGN KEY (visitor_id)
		REFERENCES visitor (visitor_id)
        ON DELETE CASCADE
);

CREATE TABLE issuance_employee(
    issuance_id SERIAL,
    employee_id SERIAL,
    FOREIGN KEY (employee_id)
        REFERENCES employee (employee_id)
        ON DELETE CASCADE,
    FOREIGN KEY (issuance_id)
        REFERENCES issuance (issuance_id)
        ON DELETE CASCADE,
    PRIMARY KEY (issuance_id, employee_id)
);


CREATE TABLE publisher(
    publisher_id SERIAL PRIMARY KEY,
    publisher_title VARCHAR(200),
    city VARCHAR(50)
);

CREATE TABLE genre(
    genre_id SERIAL PRIMARY KEY,
    genre_title VARCHAR(200)
);

CREATE TABLE book(
    book_id SERIAL PRIMARY KEY,
    title VARCHAR(200),
    author_id INT,
    series_id INT,
    country_id INT,
    place_id INT,
    publisher_id INT,
    issuance_id INT,  
    genre_id INT,
    number_of_pages INT,
	FOREIGN KEY (author_id)
        REFERENCES author (author_id)
        ON DELETE CASCADE,
	FOREIGN KEY (series_id) 
        REFERENCES series (series_id) 
        ON DELETE CASCADE,
	FOREIGN KEY (country_id)
		REFERENCES country (country_id)
		ON DELETE CASCADE,
	FOREIGN KEY (place_id)
        REFERENCES place (place_id)
        ON DELETE CASCADE,
	FOREIGN KEY (publisher_id)
		REFERENCES publisher (publisher_id)
		ON DELETE CASCADE,
	FOREIGN KEY (genre_id)
		REFERENCES genre (genre_id)
		ON DELETE CASCADE		
);

CREATE TABLE books_issuance(
    book_id SERIAL,
    issuance_id SERIAL,
    PRIMARY KEY(book_id, issuance_id),
    FOREIGN KEY (issuance_id)
        REFERENCES issuance (issuance_id)
        ON DELETE CASCADE,
    FOREIGN KEY (book_id)
        REFERENCES book (book_id)
        ON DELETE CASCADE
);


INSERT INTO employee(last_name,first_name,middle_name, employee_phone_number, position) VALUES
('Одинцов', 'Марк', 'Кириллович','+7(058)953-8787','Библиотекарь'),
('Андреев', 'Ян', 'Еремеевич','+7(472)172-5301','Библиотекарь'),
('Зимин', 'Рудольф', 'Артёмович','+7(318)804-8460','Библиотекарь'),
('Горбунов', 'Болеслав', 'Тихонович','+7(978)425-9567','Библиотекарь'),
('Семёнов', 'Август', 'Георгиевич','+7(648)247-2798','Главный библиотекарь'),
('Калинин', 'Гордий', 'Созонович','+7(970)189-9144','Директор'),
('Гаврилова', 'Юна', 'Тимофеевна','+7(146)927-6750','Помощник директора'),
('Кулагина', 'Неолина', 'Николаевна','+7(295)398-8608','Библиограф'),
('Виноградова', 'Анна', 'Даниловна','+7(922)453-9052','Уборщица'),
('Андреева', 'Ярославна', 'Георгиевна','+7(950)648-7531','Научный сотрудник библиотеки');

INSERT INTO genre(genre_title) VALUES
('Роман'),('Фэнтези'),('Детектив'),('Приключение'),('Триллер'),
('Проза'),('Ужасы'),('История'),('Научная Литература'),('Мемуары');

INSERT INTO author(first_name,middle_name,last_name,country,date_of_birth,date_of_death) VALUES
('Анджей','','Сапковский','Польша','1948-06-21','-'),
('Джоан', 'Кэтлин', 'Роулинг','Великобритания','1965-07-31','-'),
('Дмитрий', 'Алексеевич', 'Глуховский','Россия','1979-06-12','-'),
('Артур', 'Конон', 'Дойл','Великобритания','1859-05-22','1930-07-07'),
('Фёдор', 'Михайлович', 'Достаевский','Россия','1821-11-11','1881-02-09'),
('Лев', 'Николаевич', 'Толстой', 'Россия','1828-09-09','1910-11-20'),
('Жюль', 'Габриель', 'Верн', 'Франция','1828-02-08','1905-03-24'),
('Александр', 'Сергеевич', 'Пушкин', 'Россия','1799-06-06','1837-02-10');

INSERT INTO series(title, number_of_books) VALUES
('',1),
('Ведьмак',7),
('Гарри Поттер',7),
('Метро 2033',3);

INSERT INTO country(country_name) VALUES
('Россия'),
('США'),
('Великобритания'),
('Чехия'),
('Польша'),
('Германия'),
('Бельгия');

INSERT INTO publisher(publisher_title,city) VALUES 
('Эксмо','Москва'),
('Springer','США'),
('АСТ','Санкт-Петербург'),
('Триумф','Москва'),
('American Medical Association','США'),
('Азбука-Аттикус','Москва'),
('Росмэн','Санкт-Петербург'),
('Birkhдuser Verlag','Великобритания'),
('Альпина Паблишер','Санкт-Петербург');

INSERT INTO place(room,line,shelf,position) VALUES
(1,1,1,1),(1,9,3,1),(1,5,4,8),(1,4,2,1),(1,4,2,3),(1,4,8,8),
(2,1,1,1),(2,4,5,2),(2,2,8,5),(2,6,9,1),
(3,5,6,1),(3,5,7,8),(3,7,3,6),
(4,1,6,9),(4,3,1,5),(4,2,9,5),(4,7,2,1),
(5,6,2,4),(5,6,6,6);

INSERT INTO visitor(issuance_id, last_name,first_name,middle_name, addres, visitor_phone_number) VALUES
(1, 'Григорий', 'Валентинович', 'Фомин', 'Москва, Улица Пушкина, 17, квартира 5', '+7 (999) 123-45-67'),
(2, 'Сергей', 'Иванович', 'Иванов', 'Москва, Проспект Ленинградский, 14, квартира 9', '+7 (987) 654-32-10'),
(3, 'Елена', 'Александровна', 'Баранова','Москва, Улица Ленина, 10, квартира 3','+7 (926) 111-22-33'),
(4, 'Андрей', 'Игоревич', 'Жуковский','Москва, Проспект Октября, 25, квартира 7','+7 (495) 555-44-33'),
(5, 'Наталья', 'Дмитриевна', 'Кузьмина','Москва, Улица Гагарина, 7А, квартира 12','+7 (812) 777-88-99'),
(6, 'Вячеслав', 'Федорович', 'Попов','Москва, Переулок Строителей, 15, квартира 6','+7 (903) 333-22-11'),
(7, 'Ольга', 'Сергеевна', 'Семенова','Москва, Улица Лермонтова, 3, квартира 4','+7 (800) 123-45-67'),
(8, 'Алексей', 'Павлович', 'Родионов','Москва, Проспект Мира, 9, квартира 8','+7 (499) 987-65-43'),
(9, 'Марина', 'Викторовна', 'Щербакова','Москва, Улица Чехова, 12, квартира 2','+7 (985) 111-11-11'),
(10, 'Владимир', 'Ярославович', 'Ткачев','Москва, Переулок Кирова, 6, квартира 11','+7 (911) 555-55-55');

INSERT INTO issuance(visitor_id, date_of_issuance, date_of_return) VALUES
(1, '2023-10-02', '2023-10-05'),
(2, '2023-09-10', '2023-09-18'),
(3,'2023-10-01','2023-10-10'),
(4,'2023-08-15','2023-08-29'),
(5,'2023-05-20','2023-06-16'),
(6,'2023-04-02','2023-04-18'),
(7,'2023-09-12','2023-09-19'),
(8,'2023-07-07','2023-07-29'),
(9,'2023-03-25','2023-04-16'),
(10,'2023-06-10','2023-07-04');

INSERT INTO issuance_employee(issuance_id, employee_id) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);

INSERT INTO book(title,author_id,series_id,country_id,place_id,publisher_id,issuance_id,genre_id,number_of_pages) VALUES 
('Филосовский камень',2,3,3,1,4,1,2,432),
('Тайная комната',2,3,3,2,3,2,2,480),
('Узник Азкабана',2,3,3,3,1,3,2,328),
('Последнее желание',1,2,6,4,7,4,4,640),
('Кровь эльфов',1,2,6,5,8,5,4,480),
('Час Презрения',1,2,6,6,4,6,4,464),
('Ледяной плен',3,4,1,7,9,7,4,370),
('Последний поход',3,4,1,8,2,8,4,380),
('Анна Каренина',6,1,1,9,5,9,1,799),
('Евгений Онегин',8,1,1,10,2,10,1,496);

INSERT INTO books_issuance(book_id, issuance_id) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);


































