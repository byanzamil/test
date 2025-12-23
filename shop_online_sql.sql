DROP TABLE customers;
DROP TABLE products;
DROP TABLE orders;
DROP TABLE order_items;
create table customers (
	id SERIAL PRIMARY KEY, 
	full_name TEXT NOT NULL,
	email VARCHAR NOT NULL
);

create table products (
	id SERIAL PRIMARY KEY, 
	name TEXT NOT NULL,
	price FLOAT NOT NULL,
	stock INT NOT NULL
);

create table orders (
	id SERIAL PRIMARY KEY,
	customer_id SERIAL NOT NULL,
	order_date VARCHAR NOT NULL
);

create table order_items (
	order_id SERIAL NOT NULL,
	product_id SERIAL NOT NULL,
	quantity INT NOT NULL
);



INSERT INTO customers (id,full_name, email)
values 
(1, 'Lina Ahmad', 'lina@mail.com'),
(2, 'Omar Saleh', 'omar@mail.com'),
(3, 'Sara Khalil', 'sara@mail.com'),
(4, 'Byan zamil', 'byanzamil@gmail.com')
;

INSERT INTO products (id, name, price, stock)
values
(1, 'Laptop', 900, 5),
(2, 'mouse', 25, 20),
(3, 'Keyboard', 60, 10),
(4, 'Monitor', 300, 7)
;

INSERT INTO orders (id, customer_id, order_date)
values
(1, 1, '30/12/2025'),
(2, 1, '18/01/2026'),
(3, 2, '01/01/2026'),
(4, 3, '10/02/2026')
;

INSERT INTO order_items (order_id, product_id, quantity)
values 
(1, 1, 1),
(1, 3, 2),
(2, 2, 3),
(2, 4, 1),
(3, 3, 1),
(3, 2, 2),
(3, 4, 1),
(4, 1, 1),
(4, 3, 1),
(4, 2, 3),
(4, 4, 1)
;


SELECT o.id as order_id, c.id as customer_id, c.full_name as customer_name,  SUM(oi.quantity*p.price) as total_price 
FROM  orders o
INNER JOIN customers c ON o.customer_id = c.id
INNER JOIN order_items oi ON o.id = oi.order_id
INNER JOIN products p ON oi.product_id = p.id
GROUP BY o.id, c.id
ORDER BY o.id;

SELECT c.id as customer_id, c.full_name as customer_name, SUM(oi.quantity*p.price) as total_price 
FROM  customers c
INNER JOIN orders o ON o.customer_id = c.id
INNER JOIN order_items oi ON o.id = oi.order_id
INNER JOIN products p ON oi.product_id = p.id
GROUP BY c.id, c.full_name
ORDER BY c.id;

SELECT p.id, p.name, SUM(oi.quantity) as total_quantity
FROM order_items oi
INNER JOIN products p ON oi.product_id = p.id
GROUP BY p.id
ORDER BY total_quantity DESC;

SELECT c.id as cutomer_id, c.full_name as customer_name
FROM customers c
WHERE NOT EXISTS (SELECT *
				FROM orders o
				WHERE c.id = o.customer_id);