SELECT top 5 * From production.products 
order by list_price desc

select count(*) 
from sales.customers
where phone is null

select count(order_id) AS orderNum,sales.customers.customer_id, sales.customers.first_name,sales.customers.last_name
from sales.orders inner join sales.customers on sales.orders.customer_id  = sales.customers.customer_id
group by sales.customers.customer_id,sales.customers.first_name,sales.customers.last_name
HAVING count(order_id) > 1

select count(*) 
from sales.orders inner join sales.customers on sales.orders.customer_id  =sales.customers.customer_id
where sales.customers.first_name = 'Daryl' and sales.customers.last_name = 'Spence' 



select count(order_id) AS orderNum
from sales.orders 
group by sales.orders.customer_id
HAVING count(order_id) > 1

SELECT COUNT(orderNum) 
FROM (
select count(order_id) AS orderNum
from sales.orders 
group by sales.orders.customer_id
HAVING count(order_id) > 1) AS SOMETABLE


SELECT p.product_name as Name 
from production.products as p
inner join production.stocks as s on s.product_id = p.product_id
where s.quantity = 0 and s.store_id = 1

SELECT s.product_id, sum(s.quantity)
from production.stocks as s
group by s.product_id
having sum(s.quantity) = 0 

SELECT p.product_name as productName, st.store_name
from production.stocks as s 
inner join production.products as p on s.product_id = p.product_id
inner join sales.stores as st on s.store_id = st.store_id
where s.quantity = 0 

--update production.stocks 
--set quantity = 0
--where product_id = 1

(SELECT p.product_name
FROM production.products p
JOIN production.stocks s
ON p.product_id=s.product_id
WHERE s.quantity=0 AND s.store_id=1
INTERSECT
SELECT p.product_name
FROM production.products p
JOIN production.stocks s
ON p.product_id=s.product_id
WHERE s.quantity=0 AND s.store_id=2
INTERSECT
SELECT p.product_name
FROM production.products p
JOIN production.stocks s
ON p.product_id=s.product_id
WHERE s.quantity=0 AND s.store_id=3)


select sum(oi.list_price*oi.quantity) as totalCost
from sales.order_items as oi
where oi.order_id = 1

select sum(oi.list_price*oi.quantity) as totalCost, oi.order_id
from sales.order_items as oi
GROUP BY oi.order_id 
having sum(oi.list_price*oi.quantity) > 10000


select sum(oi.list_price*oi.quantity*(1-oi.discount)) as totalCost, oi.order_id
from sales.order_items as oi
GROUP BY oi.order_id 