ALTER TABLE rentals ADD COLUMN BookingID INT NULL;
ALTER TABLE rentals ADD CONSTRAINT FK_Rentals_Bookings_BookingID FOREIGN KEY (BookingID) REFERENCES bookings(BookingId) ON DELETE SET NULL ON UPDATE CASCADE;


INSERT INTO Rentals (
    VehicleID, RentalCost, VehicleName, VehicleType, State, Country, 
    CarInsurance, LicensePlate, MakeModel, Year, Mileage,
    Description, StartDate, EndDate, PickupLocation, DropoffLocation,
    Status, Availability, GpsIncluded, DriverAge, AdditionalDriverOption
) VALUES
(1, 112, 'Hyundai Elantra', 'Coupe', 'Nunavut', 'Canada', 1, '266-XYZ', 'Hyundai Elantra', 2018, 42292, 'Excellent condition', '2024-02-25', '2024-03-02', 'Airport', 'Downtown', 'Rented', FALSE, TRUE, 0, 0),
(2, 51, 'Ford Fusion', 'Sedan', 'Yukon', 'Canada', 0, '863-XYZ', 'Ford Fusion', 2017, 39222, 'Brand new', '2024-02-27', '2024-02-28', 'Airport', 'Downtown', 'Maintenance', TRUE, FALSE, 0, 0),
(3, 55, 'Hyundai Elantra', 'Coupe', 'Yukon', 'Canada', 0, '736-XYZ', 'Hyundai Elantra', 2018, 11423, 'Slightly used', '2024-02-22', '2024-02-26', 'Airport', 'Downtown', 'Maintenance', TRUE, TRUE, 0, 1),
(4, 116, 'Ford Fusion', 'Hatchback', 'British Columbia', 'Canada', 1, '232-XYZ', 'Ford Fusion', 2020, 35405, 'Slightly used', '2024-02-21', '2024-02-27', 'Airport', 'Downtown', 'Available', TRUE, TRUE, 0, 1),
(5, 138, 'Hyundai Elantra', 'Coupe', 'Saskatchewan', 'Canada', 1, '992-XYZ', 'Hyundai Elantra', 2019, 31856, 'Brand new', '2024-02-24', '2024-03-04', 'Airport', 'Downtown', 'Maintenance', TRUE, FALSE, 0, 0),
(6, 89, 'Ford Fusion', 'Coupe', 'Alberta', 'Canada', 1, '979-XYZ', 'Ford Fusion', 2021, 31907, 'Excellent condition', '2024-02-26', '2024-03-03', 'Airport', 'Downtown', 'Available', TRUE, TRUE, 0, 0),
(7, 123, 'Chevrolet Impala', 'Convertible', 'Yukon', 'Canada', 0, '788-XYZ', 'Chevrolet Impala', 2021, 32530, 'Well maintained', '2024-02-28', '2024-03-02', 'Airport', 'Downtown', 'Available', FALSE, TRUE, 0, 0),
(8, 95, 'Toyota Corolla', 'SUV', 'British Columbia', 'Canada', 0, '301-XYZ', 'Toyota Corolla', 2020, 13448, 'Well maintained', '2024-02-21', '2024-02-24', 'Airport', 'Downtown', 'Available', TRUE, FALSE, 0, 1),
(9, 122, 'Chevrolet Impala', 'Hatchback', 'Yukon', 'Canada', 1, '237-XYZ', 'Chevrolet Impala', 2018, 45896, 'Excellent condition', '2024-02-28', '2024-02-27', 'Airport', 'Downtown', 'Maintenance', FALSE, FALSE, 0, 1),
(10, 93, 'Honda Civic', 'Coupe', 'Nova Scotia', 'Canada', 0, '775-XYZ', 'Honda Civic', 2015, 35966, 'Like new', '2024-02-24', '2024-03-04', 'Airport', 'Downtown', 'Maintenance', TRUE, TRUE, 0, 1);

