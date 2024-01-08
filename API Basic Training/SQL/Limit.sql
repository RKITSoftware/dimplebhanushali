-- Use Practice
 database

USE 
	Practice
;


-- Create Products table
CREATE TABLE IF NOT EXISTS Products (
    ProductID INT PRIMARY KEY AUTO_INCREMENT,
    ProductName VARCHAR(255) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL
);

insert into Products (ProductName, price) values ('Beans - Kidney White', 230.38);
insert into Products (ProductName, price) values ('Cattail Hearts', 146.24);
insert into Products (ProductName, price) values ('Beans - Navy, Dry', 155.92);
insert into Products (ProductName, price) values ('Ecolab - Hobart Washarm End Cap', 386.26);
insert into Products (ProductName, price) values ('Ecolab Crystal Fusion', 292.07);
insert into Products (ProductName, price) values ('Cheese - Marble', 190.93);
insert into Products (ProductName, price) values ('Chips - Miss Vickies', 498.72);
insert into Products (ProductName, price) values ('Flour - So Mix Cake White', 425.11);
insert into Products (ProductName, price) values ('Flower - Commercial Spider', 232.44);
insert into Products (ProductName, price) values ('Tomatoes - Plum, Canned', 273.0);
insert into Products (ProductName, price) values ('Lamb - Leg, Boneless', 427.04);
insert into Products (ProductName, price) values ('Ecolab - Lime - A - Way 4/4 L', 365.43);
insert into Products (ProductName, price) values ('Mustard - Seed', 432.52);
insert into Products (ProductName, price) values ('Bread - Petit Baguette', 242.75);
insert into Products (ProductName, price) values ('Yoplait - Strawbrasp Peac', 411.43);
insert into Products (ProductName, price) values ('Seedlings - Clamshell', 387.83);
insert into Products (ProductName, price) values ('Thyme - Fresh', 441.92);
insert into Products (ProductName, price) values ('Cassis', 126.38);
insert into Products (ProductName, price) values ('Potatoes - Instant, Mashed', 267.02);
insert into Products (ProductName, price) values ('Iced Tea Concentrate', 386.15);
insert into Products (ProductName, price) values ('Energy Drink - Franks Original', 164.56);
insert into Products (ProductName, price) values ('Eel Fresh', 317.2);
insert into Products (ProductName, price) values ('Creme De Menth - White', 75.07);
insert into Products (ProductName, price) values ('Cheese - Swiss', 385.74);
insert into Products (ProductName, price) values ('Soup - Clam Chowder, Dry Mix', 461.78);
insert into Products (ProductName, price) values ('Appetizer - Tarragon Chicken', 128.13);
insert into Products (ProductName, price) values ('Table Cloth 81x81 Colour', 143.92);
insert into Products (ProductName, price) values ('Dip - Tapenade', 71.44);
insert into Products (ProductName, price) values ('Onions - Dried, Chopped', 217.68);
insert into Products (ProductName, price) values ('Sprouts - Baby Pea Tendrils', 326.04);
insert into Products (ProductName, price) values ('Soup - Knorr, Classic Can. Chili', 451.84);
insert into Products (ProductName, price) values ('Tomato - Plum With Basil', 181.91);
insert into Products (ProductName, price) values ('Juice - Prune', 451.22);
insert into Products (ProductName, price) values ('Beef - Outside, Round', 460.42);
insert into Products (ProductName, price) values ('Tea - Herbal Orange Spice', 392.08);
insert into Products (ProductName, price) values ('Wasabi Paste', 218.33);
insert into Products (ProductName, price) values ('Beef - Top Butt', 361.49);
insert into Products (ProductName, price) values ('Sea Bass - Fillets', 260.14);
insert into Products (ProductName, price) values ('Flavouring - Rum', 455.8);
insert into Products (ProductName, price) values ('Venison - Ground', 306.69);
insert into Products (ProductName, price) values ('Chocolate - Dark', 237.04);
insert into Products (ProductName, price) values ('Mix - Cocktail Strawberry Daiquiri', 362.16);
insert into Products (ProductName, price) values ('Shiro Miso', 230.87);
insert into Products (ProductName, price) values ('Table Cloth 90x90 Colour', 73.61);
insert into Products (ProductName, price) values ('Oil - Peanut', 250.78);
insert into Products (ProductName, price) values ('Onions - Green', 73.01);
insert into Products (ProductName, price) values ('Bread - Bistro White', 363.11);
insert into Products (ProductName, price) values ('Flavouring - Rum', 435.17);
insert into Products (ProductName, price) values ('Pasta - Bauletti, Chicken White', 107.54);
insert into Products (ProductName, price) values ('Straws - Cocktale', 450.54);
insert into Products (ProductName, price) values ('Salmon - Atlantic, No Skin', 498.72);
insert into Products (ProductName, price) values ('Chicken - Base', 466.21);
insert into Products (ProductName, price) values ('Coffee Beans - Chocolate', 348.02);
insert into Products (ProductName, price) values ('Bag - Regular Kraft 20 Lb', 244.7);
insert into Products (ProductName, price) values ('Shrimp - 150 - 250', 250.37);
insert into Products (ProductName, price) values ('Whmis Spray Bottle Graduated', 272.29);
insert into Products (ProductName, price) values ('Pasta - Penne, Rigate, Dry', 490.93);
insert into Products (ProductName, price) values ('Teriyaki Sauce', 339.78);
insert into Products (ProductName, price) values ('Wine - Riesling Alsace Ac 2001', 147.57);
insert into Products (ProductName, price) values ('Samosa - Veg', 309.97);
insert into Products (ProductName, price) values ('Egg Patty Fried', 195.37);
insert into Products (ProductName, price) values ('Jagermeister', 159.87);
insert into Products (ProductName, price) values ('Pineapple - Canned, Rings', 185.42);
insert into Products (ProductName, price) values ('Chilli Paste, Ginger Garlic', 324.29);
insert into Products (ProductName, price) values ('Pork - Smoked Kassler', 194.28);
insert into Products (ProductName, price) values ('Dried Cherries', 489.62);
insert into Products (ProductName, price) values ('Spring Roll Wrappers', 469.21);
insert into Products (ProductName, price) values ('Pastry - Cherry Danish - Mini', 411.54);
insert into Products (ProductName, price) values ('Cheese - Mix', 94.49);
insert into Products (ProductName, price) values ('Roe - Lump Fish, Black', 125.59);
insert into Products (ProductName, price) values ('Rum - Light, Captain Morgan', 336.84);
insert into Products (ProductName, price) values ('Beer - Sleemans Cream Ale', 370.13);
insert into Products (ProductName, price) values ('Tuna - Salad Premix', 110.39);
insert into Products (ProductName, price) values ('Beef - Rib Roast, Cap On', 397.22);
insert into Products (ProductName, price) values ('Oil - Coconut', 113.12);
insert into Products (ProductName, price) values ('Oil - Sesame', 482.85);
insert into Products (ProductName, price) values ('Ice Cream Bar - Oreo Sandwich', 365.81);
insert into Products (ProductName, price) values ('Yogurt - Raspberry, 175 Gr', 158.98);
insert into Products (ProductName, price) values ('Cheese - Manchego, Spanish', 236.48);
insert into Products (ProductName, price) values ('Beer - Sleemans Cream Ale', 200.82);
insert into Products (ProductName, price) values ('Cheese - Havarti, Salsa', 78.49);
insert into Products (ProductName, price) values ('Pie Filling - Pumpkin', 394.26);
insert into Products (ProductName, price) values ('Table Cloth 54x54 White', 149.97);
insert into Products (ProductName, price) values ('Red Snapper - Fillet, Skin On', 185.49);
insert into Products (ProductName, price) values ('Bread - Crumbs, Bulk', 447.48);
insert into Products (ProductName, price) values ('Butter Balls Salted', 489.67);
insert into Products (ProductName, price) values ('Sauce - Sesame Thai Dressing', 165.26);
insert into Products (ProductName, price) values ('Chicken - Whole Fryers', 77.3);
insert into Products (ProductName, price) values ('Scallops 60/80 Iqf', 383.76);
insert into Products (ProductName, price) values ('Wine - Bourgogne 2002, La', 304.38);
insert into Products (ProductName, price) values ('Soy Protein', 382.28);
insert into Products (ProductName, price) values ('Mushroom - Chantrelle, Fresh', 262.13);
insert into Products (ProductName, price) values ('Lettuce - California Mix', 336.08);
insert into Products (ProductName, price) values ('Avocado', 159.72);
insert into Products (ProductName, price) values ('Bread - Roll, Soft White Round', 373.06);
insert into Products (ProductName, price) values ('Veal - Eye Of Round', 214.2);
insert into Products (ProductName, price) values ('Meldea Green Tea Liquor', 354.63);
insert into Products (ProductName, price) values ('Beer - True North Strong Ale', 154.32);
insert into Products (ProductName, price) values ('Wine - Malbec Trapiche Reserve', 476.81);
insert into Products (ProductName, price) values ('Pepsi, 355 Ml', 276.85);

-- Showing all records

SELECT
	ProductID,
    ProductName,
    Price
FROM
	Products;
    
-- Limit

-- Only showing 50 records

SELECT
	ProductID,
    ProductName,
    Price
FROM
	Products
LIMIT 
	50;

-- Using Where Clause

SELECT
	ProductID,
    ProductName,
    Price
FROM
	Products
WHERE
	Price > 50
LIMIT 
	10;

-- Using Offset
-- I want to start list of records from 11 ProductID

SELECT
	ProductID,
    ProductName,
    Price
FROM
	Products
LIMIT 
	50
OFFSET		-- Neglecting firxt 10 records
	10; 
    

