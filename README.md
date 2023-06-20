# Hotel-Booking

## Database
For using database locally you have to run docker commands discribed below:

Create docker image:
```
docker image build -t hotelbooking .
```

Create and run docker container:
```
docker container run -d -p 1433:1433 --name HotelBookingDb hotelbooking
```

Remember to invoking the commands from location where dockerfile is located.