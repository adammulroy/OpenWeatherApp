# Weather API

# Geocoding
## Zip Code
### Endpoint
* `http://api.openweathermap.org/geo/1.0/zip?zip={zip code},{country code}&appid=[{API key}
	
## City Name
* `http://api.openweathermap.org/geo/1.0/direct?q={city name},{state code},{country code}&limit={limit}&appid=[{API key}]
	* No need to include limit
	* Limiting to US only due to API call limit
	* Testing seems fine to not include the state code
	* Matches are not very fuzzy
		* Most tests require almost exact matches in most cases
## Coordinates
* `http://api.openweathermap.org/geo/1.0/reverse?lat={lat}&lon={lon}&limit={limit}&appid=[{API key}]
	* No need to include limit
	
# Weather

* CurrentWeather
	* `https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid=[{API key}](https://home.openweathermap.org/api_keys)`
	





