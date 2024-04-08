INSERT INTO Hotels (
    HotelId,
    HotelName,
    Address,
    City,
    PostalCode,
    State,
    Country,
    CostPerNight,
    StartDate,
    EndDate,
    Status,
    StarRating,
    RoomType,
    NumberOfRooms,
    MaxOccupancy,
    PhoneNumber,
    CancellationPolicy,
    CheckInTime,
    CheckOutTime,
    Description,
    image
) VALUES (
    1, -- HotelId
    'The Ritz Carlton', -- HotelName
    '181 Wellington St W', -- Address
    'Toronto', -- City
    'M5V 3G7', -- PostalCode
    'Ontario', -- State
    'Canada', -- Country
    410, -- CostPerNight
    '2024-01-01', -- StartDate
    '2030-12-31', -- EndDate
    'Open', -- Status
    5, -- StarRating
    'Master Suite', -- RoomType
    500, -- NumberOfRooms
    3000, -- MaxOccupancy
    '416-274-7890', -- PhoneNumber
    '24H Before', -- CancellationPolicy (nullable)
    '15:00', -- CheckInTime
    '11:00', -- CheckOutTime
    "At The Ritz-Carlton, Toronto, you will discover what it means when a luxury hotel. Located near the CN Tower and Rogers Centre, The Ritz Carlton is Toronto's most luxurious stay.", -- Description
    "../img/ritz.webp" -- Image Path
    
);

INSERT INTO Hotels (
    HotelId,
    HotelName,
    Address,
    City,
    PostalCode,
    State,
    Country,
    CostPerNight,
    StartDate,
    EndDate,
    Status,
    StarRating,
    RoomType,
    NumberOfRooms,
    MaxOccupancy,
    PhoneNumber,
    CancellationPolicy,
    CheckInTime,
    CheckOutTime,
    Description,
    image
) VALUES (
	2, -- HotelId
    'The Sutton Place', -- HotelName
    '1700 Grafton Street', -- Address
    'Halifax', -- City
    'B3J 2C4', -- PostalCode
    'Nova Scotia', -- State
    'Canada', -- Country
    210, -- CostPerNight
    '2024-01-01', -- StartDate
    '2030-12-31', -- EndDate
    'Open', -- Status
    4, -- StarRating
    'Suite', -- RoomType
    350, -- NumberOfRooms
    2000, -- MaxOccupancy
    '184-375-2745', -- PhoneNumber
    '24H Before', -- CancellationPolicy (nullable)
    '15:00', -- CheckInTime
    '11:00', -- CheckOutTime
    "At The Sutton Place Hotel located in Halifax, guests will have an exceptional stay. Located only a few minutes away from the Scotiabank Centre, Casino Nova Scotia and The Boardwalk.", -- Description
    "../img/sutton.webp" -- Image
);

INSERT INTO Hotels (
	HotelId,
    HotelName,
    Address,
    City,
    PostalCode,
    State,
    Country,
    CostPerNight,
    StartDate,
    EndDate,
    Status,
    StarRating,
    RoomType,
    NumberOfRooms,
    MaxOccupancy,
    PhoneNumber,
    CancellationPolicy,
    CheckInTime,
    CheckOutTime,
    Description,
    image
) VALUES (
	3, -- HotelId
    'Le Mount Stephen', -- HotelName
    '1440 Drummond Street', -- Address
    'Montreal', -- City
    'H3G 1V9', -- PostalCode
    'Quebec', -- State
    'Canada', -- Country
    260, -- CostPerNight
    '2024-01-01', -- StartDate
    '2030-12-31', -- EndDate
    'Open', -- Status
    4, -- StarRating
    'Suite', -- RoomType
    350, -- NumberOfRooms
    2000, -- MaxOccupancy
    '184-375-2745', -- PhoneNumber
    '24H Before', -- CancellationPolicy (nullable)
    '15:00', -- CheckInTime
    '11:00', -- CheckOutTime
    "Le Mount Stephen is located in Downtown Montreal. Guests are located close by to The Underground City, YUL Airport, Bell Centre and The Museum of Fine Arts.", -- Description
    "../img/Mount-Stephen.webp" -- Image
);

INSERT INTO Hotels (
	HotelId,
    HotelName,
    Address,
    City,
    PostalCode,
    State,
    Country,
    CostPerNight,
    StartDate,
    EndDate,
    Status,
    StarRating,
    RoomType,
    NumberOfRooms,
    MaxOccupancy,
    PhoneNumber,
    CancellationPolicy,
    CheckInTime,
    CheckOutTime,
    Description,
    image
) VALUES (
	4, -- HotelId
    'Rimrock Resort Hotel', -- HotelName
    '300 Moutain Avenue', -- Address
    'Banff', -- City
    'T1L 1J2', -- PostalCode
    'Alberta', -- State
    'Canada', -- Country
    350, -- CostPerNight
    '2024-01-01', -- StartDate
    '2030-12-31', -- EndDate
    'Open', -- Status
    5, -- StarRating
    'Master', -- RoomType
    350, -- NumberOfRooms
    2000, -- MaxOccupancy
    '184-375-2745', -- PhoneNumber
    '24H Before', -- CancellationPolicy (nullable)
    '15:00', -- CheckInTime
    '11:00', -- CheckOutTime
    "Rimrock Resort Hotel Banff is located in Sulphur Mountain District, in the mountains and in a national park. Local activites include the Banff Upper Hot Springs, Banff Gondola and the Lake Louise Tourism Bureau.", -- Description
    "../img/rimrock.webp" -- Image
);

INSERT INTO Hotels (
	HotelId,
    HotelName,
    Address,
    City,
    PostalCode,
    State,
    Country,
    CostPerNight,
    StartDate,
    EndDate,
    Status,
    StarRating,
    RoomType,
    NumberOfRooms,
    MaxOccupancy,
    PhoneNumber,
    CancellationPolicy,
    CheckInTime,
    CheckOutTime,
    Description,
    image
) VALUES (
	5, -- HotelId
    'Sheraton Hotel', -- HotelName
    '115 Cavendish Square', -- Address
    'St. Johns', -- City
    'A1C 3K2', -- PostalCode
    'Newfoundland', -- State
    'Canada', -- Country
    210, -- CostPerNight
    '2024-01-01', -- StartDate
    '2030-12-31', -- EndDate
    'Open', -- Status
    5, -- StarRating
    'Queen', -- RoomType
    350, -- NumberOfRooms
    2000, -- MaxOccupancy
    '184-375-2745', -- PhoneNumber
    '24H Before', -- CancellationPolicy (nullable)
    '15:00', -- CheckInTime
    '11:00', -- CheckOutTime
    "Located in St. John's, Sheraton Hotel Newfoundland is in the city center. Marine Drive Provincial Park and Avalon Peninsula reflect the area's natural beauty and area attractions include Johnson Geo Centre and Mile One Centre.", -- Description
    "../img/sheraton-nfld.webp" -- Image
);

INSERT INTO Hotels (
     HotelId,
    HotelName,
    Address,
    City,
    PostalCode,
    State,
    Country,
    CostPerNight,
    StartDate,
    EndDate,
    Status,
    StarRating,
    RoomType,
    NumberOfRooms,
    MaxOccupancy,
    PhoneNumber,
    CancellationPolicy,
    CheckInTime,
    CheckOutTime,
    Description,
    image
) VALUES (
	6, -- HotelId
    'JW Marriott Parq', -- HotelName
    '39 Smithe Street', -- Address
    'Vancouver', -- City
    'V6B 0R3', -- PostalCode
    'British Columbia', -- State
    'Canada', -- Country
    300, -- CostPerNight
    '2024-01-01', -- StartDate
    '2030-12-31', -- EndDate
    'Open', -- Status
    5, -- StarRating
    'Suite', -- RoomType
    350, -- NumberOfRooms
    2000, -- MaxOccupancy
    '184-375-2745', -- PhoneNumber
    '24H Before', -- CancellationPolicy (nullable)
    '15:00', -- CheckInTime
    '11:00', -- CheckOutTime
    "JW Marriott Parq Vancouver is located in Downtown Vancouver, and is in the entertainment district and on the waterfront. Local attractions include BC Place Stadium, Rogers Arena, Canada Place and Stanley Park.", -- Description
    "../img/Marriot-Vanc.webp" -- Image
);

INSERT INTO Hotels (
     HotelId,
    HotelName,
    Address,
    City,
    PostalCode,
    State,
    Country,
    CostPerNight,
    StartDate,
    EndDate,
    Status,
    StarRating,
    RoomType,
    NumberOfRooms,
    MaxOccupancy,
    PhoneNumber,
    CancellationPolicy,
    CheckInTime,
    CheckOutTime,
    Description,
    image
) VALUES (
	7, -- HotelId
    'Dakota Dunes Resort', -- HotelName
    '203 Dakota Dunes Way', -- Address
    'Whitecap', -- City
    'S7K 2L2', -- PostalCode
    'Saskatchewan', -- State
    'Canada', -- Country
    150, -- CostPerNight
    '2024-01-01', -- StartDate
    '2030-12-31', -- EndDate
    'Open', -- Status
    5, -- StarRating
    'Suite', -- RoomType
    200, -- NumberOfRooms
    1200, -- MaxOccupancy
    '184-375-2745', -- PhoneNumber
    '24H Before', -- CancellationPolicy (nullable)
    '15:00', -- CheckInTime
    '11:00', -- CheckOutTime
    "Dakota Dunes Resort is located in Whitecap. Dakota Dunes Casino and Golf Course and Midtown Plaza are two other places to visit that come recommended. Take an opportunity to explore the area for outdoor excitement like horse riding.", -- Description
    "../img/Dunes.webp" -- Image
);

INSERT INTO Hotels (
     HotelId,
    HotelName,
    Address,
    City,
    PostalCode,
    State,
    Country,
    CostPerNight,
    StartDate,
    EndDate,
    Status,
    StarRating,
    RoomType,
    NumberOfRooms,
    MaxOccupancy,
    PhoneNumber,
    CancellationPolicy,
    CheckInTime,
    CheckOutTime,
    Description,
    image
) VALUES (
	8, -- HotelId
    'The Holman Grand Hotel', -- HotelName
    '123 Grafton St', -- Address
    'Charlottetown', -- City
    'C1A 1K9', -- PostalCode
    'PEI', -- State
    'Canada', -- Country
    165, -- CostPerNight
    '2024-01-01', -- StartDate
    '2030-12-31', -- EndDate
    'Open', -- Status
    5, -- StarRating
    'Suite', -- RoomType
    200, -- NumberOfRooms
    1200, -- MaxOccupancy
    '184-375-2745', -- PhoneNumber
    '24H Before', -- CancellationPolicy (nullable)
    '15:00', -- CheckInTime
    '11:00', -- CheckOutTime
    "Located near a boardwalk/promenade, The Holman Grand Hotel is in Queens Square neighborhood and is connected to a shopping center. Confederation Centre of the Arts and The Mack are local cultural highlights located nearby.", -- Description
    "../img/Holtman.webp" -- Image
);

INSERT INTO Hotels (
     HotelId,
    HotelName,
    Address,
    City,
    PostalCode,
    State,
    Country,
    CostPerNight,
    StartDate,
    EndDate,
    Status,
    StarRating,
    RoomType,
    NumberOfRooms,
    MaxOccupancy,
    PhoneNumber,
    CancellationPolicy,
    CheckInTime,
    CheckOutTime,
    Description,
    image
) VALUES (
	9, -- HotelId
    'Chateau Fredericton by Wyndham', -- HotelName
    '470 Bishop Drive', -- Address
    'Fredericton', -- City
    'E3C 2M6', -- PostalCode
    'New Brunswick', -- State
    'Canada', -- Country
    130, -- CostPerNight
    '2024-01-01', -- StartDate
    '2030-12-31', -- EndDate
    'Open', -- Status
    4, -- StarRating
    'Suite', -- RoomType
    200, -- NumberOfRooms
    1200, -- MaxOccupancy
    '184-375-2745', -- PhoneNumber
    '24H Before', -- CancellationPolicy (nullable)
    '15:00', -- CheckInTime
    '11:00', -- CheckOutTime
    "Located in Fredericton, Chateau Fredericton by Wyndham is in an area with good shopping. Science East and The Playhouse Fredericton are cultural highlight located nearby. Also included is an indoor waterpark for family fun.", -- Description
    "../img/Hampton-NB.webp" -- Image
);

INSERT INTO Hotels (
     HotelId,
    HotelName,
    Address,
    City,
    PostalCode,
    State,
    Country,
    CostPerNight,
    StartDate,
    EndDate,
    Status,
    StarRating,
    RoomType,
    NumberOfRooms,
    MaxOccupancy,
    PhoneNumber,
    CancellationPolicy,
    CheckInTime,
    CheckOutTime,
    Description,
    image
) VALUES (
	10, -- HotelId
    'Inn At The Forks', -- HotelName
    '75 Forks Market Rd', -- Address
    'Winnipeg', -- City
    'R3C 0A2', -- PostalCode
    'Manitoba', -- State
    'Canada', -- Country
    150, -- CostPerNight
    '2024-01-01', -- StartDate
    '2030-12-31', -- EndDate
    'Open', -- Status
    4, -- StarRating
    'Suite', -- RoomType
    200, -- NumberOfRooms
    1200, -- MaxOccupancy
    '184-375-2745', -- PhoneNumber
    '24H Before', -- CancellationPolicy (nullable)
    '15:00', -- CheckInTime
    '11:00', -- CheckOutTime
    "Inn At The Forks is located in Downtown Winnipeg, near the local train station for easy commuting. Some local attractions nearby include Forks Market, RBC Convention Centre, and the Canadian Centre.", -- Description
    "../img/Fork.webp" -- Image
);

INSERT INTO Hotels (
     HotelId,
    HotelName,
    Address,
    City,
    PostalCode,
    State,
    Country,
    CostPerNight,
    StartDate,
    EndDate,
    Status,
    StarRating,
    RoomType,
    NumberOfRooms,
    MaxOccupancy,
    PhoneNumber,
    CancellationPolicy,
    CheckInTime,
    CheckOutTime,
    Description,
    image
) VALUES (
	11, -- HotelId
    'Raven Inn', -- HotelName
    'Box 31291 150 Keish Street', -- Address
    'Whitehorse', -- City
    'Y1A 5P7', -- PostalCode
    'Yukon', -- State
    'Canada', -- Country
    200, -- CostPerNight
    '2024-01-01', -- StartDate
    '2030-12-31', -- EndDate
    'Open', -- Status
    4, -- StarRating
    'Suite', -- RoomType
    200, -- NumberOfRooms
    1200, -- MaxOccupancy
    '184-375-2745', -- PhoneNumber
    '24H Before', -- CancellationPolicy (nullable)
    '15:00', -- CheckInTime
    '11:00', -- CheckOutTime
    "Raven Inn Whitehorse is located in Whitehorse. The area's natural beauty can be seen at Yukon Wildlife Preserve and Shipyards Park, while MacBride Museum of Yukon History and Old Log Church are some nearby cultural highlights.", -- Description
    "../img/Raven.webp" -- Image
);

INSERT INTO Hotels (
    HotelId,
    HotelName,
    Address,
    City,
    PostalCode,
    State,
    Country,
    CostPerNight,
    StartDate,
    EndDate,
    Status,
    StarRating,
    RoomType,
    NumberOfRooms,
    MaxOccupancy,
    PhoneNumber,
    CancellationPolicy,
    CheckInTime,
    CheckOutTime,
    Description,
    image
) VALUES (
	12, -- HotelId
    'The Explorer Hotel', -- HotelName
    '4825 49 Avenue', -- Address
    'Yellowknife', -- City
    'X1A 2R3', -- PostalCode
    'Northwest Territories', -- State
    'Canada', -- Country
    250, -- CostPerNight
    '2024-01-01', -- StartDate
    '2030-12-31', -- EndDate
    'Open', -- Status
    4, -- StarRating
    'Suite', -- RoomType
    200, -- NumberOfRooms
    1200, -- MaxOccupancy
    '184-375-2745', -- PhoneNumber
    '24H Before', -- CancellationPolicy (nullable)
    '15:00', -- CheckInTime
    '11:00', -- CheckOutTime
    "The Explorer Hotel Yellowknife is located in Yellowknife. Highlights of the region's cultural life include the Prince of Wales Northern Heritage Centre and the Northern Arts and Cultural Centre. Frame Lake Trail and Cameron River Falls Trail offer opportunities for outdoor recreation.", -- Description
    "../img/Explorer.webp" -- Image
);

INSERT INTO Hotels (
     HotelId,
    HotelName,
    Address,
    City,
    PostalCode,
    State,
    Country,
    CostPerNight,
    StartDate,
    EndDate,
    Status,
    StarRating,
    RoomType,
    NumberOfRooms,
    MaxOccupancy,
    PhoneNumber,
    CancellationPolicy,
    CheckInTime,
    CheckOutTime,
    Description,
    image
) VALUES (
	13, -- HotelId
    'Frobisher Inn', -- HotelName
    'Astro Hill Complex, PO Box 4209', -- Address
    'Iqaluit', -- City
    'X0A 0H0', -- PostalCode
    'Nunavut', -- State
    'Canada', -- Country
    330, -- CostPerNight
    '2024-01-01', -- StartDate
    '2030-12-31', -- EndDate
    'Open', -- Status
    4, -- StarRating
    'Master', -- RoomType
    200, -- NumberOfRooms
    1200, -- MaxOccupancy
    '184-375-2745', -- PhoneNumber
    '24H Before', -- CancellationPolicy (nullable)
    '15:00', -- CheckInTime
    '11:00', -- CheckOutTime
    "Frobisher Inn is located in Iqaluit. The Legislative Assembly, Waterfront, and Unikkaarvik Visitor Centre are a few of the local attractions. Take some time to explore the area's activities, such as sledding, snowshoeing, and snowmobiling.", -- Description
    "../img/Frobisher.webp" -- Image
);

INSERT INTO Hotels (
    HotelId,
    HotelName,
    Address,
    City,
    PostalCode,
    State,
    Country,
    CostPerNight,
    StartDate,
    EndDate,
    Status,
    StarRating,
    RoomType,
    NumberOfRooms,
    MaxOccupancy,
    PhoneNumber,
    CancellationPolicy,
    CheckInTime,
    CheckOutTime,
    Description,
    image
) VALUES (
	14, -- HotelId
    'Fairmont Chateau Laurier Gold Experience', -- HotelName
    '1 Rideau St-Building A', -- Address
    'Ottawa', -- City
    'K1N 8S7', -- PostalCode
    'Ontario', -- State
    'Canada', -- Country
    380, -- CostPerNight
    '2024-01-01', -- StartDate
    '2030-12-31', -- EndDate
    'Open', -- Status
    5, -- StarRating
    'Master', -- RoomType
    200, -- NumberOfRooms
    1200, -- MaxOccupancy
    '184-375-2745', -- PhoneNumber
    '24H Before', -- CancellationPolicy (nullable)
    '15:00', -- CheckInTime
    '11:00', -- CheckOutTime
    "Fairmont Chateau Laurier Gold Experience is situated in the heart of Ottawa. The Peace Tower and the Royal Canadian Mint are notable local attractions. Gatineau Water Aerodrome and Ottawa River Parkway offer some of the area's activities. Additionally worthwhile visits include Elgin Street and the Capital Information Kiosk.", -- Description
    "../img/Fairmont-OT.webp" -- Image
);

INSERT INTO Hotels (
    HotelId,
    HotelName,
    Address,
    City,
    PostalCode,
    State,
    Country,
    CostPerNight,
    StartDate,
    EndDate,
    Status,
    StarRating,
    RoomType,
    NumberOfRooms,
    MaxOccupancy,
    PhoneNumber,
    CancellationPolicy,
    CheckInTime,
    CheckOutTime,
    Description,
    image
) VALUES (
	15, -- HotelId
    'Versante Hotel', -- HotelName
    '8499 Bridgeport Rd', -- Address
    'Richmond', -- City
    'V6X 1R7', -- PostalCode
    'British Columbia', -- State
    'Canada', -- Country
    210, -- CostPerNight
    '2024-01-01', -- StartDate
    '2030-12-31', -- EndDate
    'Open', -- Status
    4, -- StarRating
    'Master', -- RoomType
    200, -- NumberOfRooms
    1200, -- MaxOccupancy
    '184-375-2745', -- PhoneNumber
    '24H Before', -- CancellationPolicy (nullable)
    '15:00', -- CheckInTime
    '11:00', -- CheckOutTime
    "The Versante Hotel is located in Richmond City Centre, with easy access to the airport and close to a metro station. If sightseeing is on the schedule, it's worth checking out Canada Place Cruise Ship Terminal and Vancouver Waterfront. Those who want to take in the natural beauty of the area can visit Stanley Park.", -- Description
    "../img/Versante.webp" -- Image
);