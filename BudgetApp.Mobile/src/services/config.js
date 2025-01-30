export const Config = {
    SERVER_ADDRESS: 'http://10.0.2.2:5007/api',
    LOGOUT_TIMEOUT_MINUTES: 30,

    getServerAddress() {
        return this.SERVER_ADDRESS;
    },

    getLogoutTimeoutMinutes() {
        return this.LOGOUT_TIMEOUT_MINUTES;
    }
}

export default Config;
// Importing the Config class in another file

// Example usage
//  console.log(Config.getApiEndpoint()); // Output: 'https://api.yourapp.com'
//  console.log(Config.getLogoutTimeoutMinutes()); // Output: 30
// Importing the Config class in another file

// Example usage
//  console.log(Config.getApiEndpoint()); // Output: 'https://api.yourapp.com'
//  console.log(Config.getLogoutTimeoutMinutes()); // Output: 30