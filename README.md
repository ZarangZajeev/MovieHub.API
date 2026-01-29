MovieHub Backend – Seat Management System
------------------------------------------------
Overview

The MovieHub Backend is a .NET Core Web API system responsible for managing movie show seats. This project focuses only on seat availability and booking behavior — it does not include payments, user authentication, movie listings, or UI components.

The system ensures accurate seat availability, prevents double bookings, and handles real-world booking challenges, including concurrent seat selections, incomplete bookings, and expired holds.

Database Design
---------------------
	The system uses PostgreSQL with the following key tables:

	1, users – Stores basic user information (id, username, guid, etc.)
	2, screens – Stores screen info (screen_name, total_seats, rows, seats_per_row)
	3, movies – Stores movie details (movie_name, duration_minutes, last_show_date)
	4, shows – Stores movie shows (movie_id, screen_id, show_time, status)
	5, seats – Stores seat info for each show (seat_row, seat_number, status: AVAILABLE/HELD/BOOKED, booking_id, held_at, held_by_user_id, held_expiry, version)
	6, bookings – Stores booking info (show_id, user_id, status: STAGED/CONFIRMED/CANCELLED, total_seats, booking_reference)

	Indexes were added to optimize queries on show_id, status, held_expiry, and held_by_user_id.

System Features
---------------------
1. View Active Shows

		Endpoint: GET /api/shows/active
		Returns all currently active shows with:
			Movie name and duration
			Show time
			Screen name and total seats

		Implemented using PostgreSQL function get_active_shows().


2. View Show Seat Details															

		Endpoint: GET /api/shows/{showId}
		Returns:
		Total seats, available seats, held seats, booked seats
		Seat arrays for each status (AVAILABLE, HELD, BOOKED)
		PostgreSQL function: get_show_seat_details(showId)


3. Hold Seats

		Endpoint: POST /api/bookings/hold
		Allows a user to temporarily hold one or more available seats before booking.
		Input: username, showId, seats (array of seat IDs, e.g., ["A1","A2"])
		PostgreSQL function: hold_seats_for_user(username, showId, seats)
		Held seats expire automatically after 20 minutes if not confirmed.


4. Confirm Booking

		Endpoint: POST /api/bookings/confirm
		Confirms held seats as booked.
		Input: username, showId, seats
		PostgreSQL function: confirm_booking(username, showId, seats)
		Returns a unique booking reference.
		Constraint: Users can only book seats that are currently held by them and not expired.


5. Fetch Booking Details by Reference

		Endpoint: GET /api/bookings/details
		Input: username, booking_reference
		Returns booking info with booked seats array.
		PostgreSQL function: get_booking_details(username, booking_reference)


6. Fetch All Bookings for a User

		Endpoint: GET /api/bookings/user?username={username}
		Returns all bookings for a user (STAGED, CONFIRMED, CANCELLED)
		Shows booked seats if confirmed.
		PostgreSQL function: get_user_bookings(username)


7. Automatic Seat Hold Cleanup

		Background service: SeatHoldCleanupService
		Runs every minute
		Releases expired HELD seats:
		Sets status = AVAILABLE
		Resets held_at, held_by_user_id, held_expiry, and version
		Ensures that stale holds do not block bookings.


Concurrency and Data Integrity
---------------------------------

	The system handles concurrency and prevents double bookings using:
	1, Row-level locking in PostgreSQL (FOR UPDATE) during seat hold and booking.
	2, Optimistic locking with the version column in seats table.
	3, Held seats expiration to avoid indefinite seat blocking.
	This ensures no seat is sold more than once.



Technologies Used
---------------------
	Backend				: .NET 8 Web API
	Database			: PostgreSQL
	Background Jobs		: .NET IHostedService
	Seat Management		: PostgreSQL stored functions




API Endpoints Summary
---------------------
Endpoint	Method	Description

	/api/shows/active		GET		Get all active shows
	/api/shows/{showId}		GET		Get show seat details
	/api/bookings/hold		POST	Hold seats for a user
	/api/bookings/confirm	POST	Confirm held seats
	/api/bookings/details	GET		Get specific booking by reference
	/api/bookings/user		GET		Get all bookings for a user



How to Run
---------------------
	Clone the repository
	Update appsettings.json with PostgreSQL connection string
	Run database scripts (tables, functions)
	Run the Web API project (dotnet run or via Visual Studio)
	Use Postman or browser to test endpoints