(function () {
    const registerAmazonClientId = () => {
        amazon.Login.setClientId('amzn1.application-oa2-client.1142949330ee4f9da51f1e4635bc8f51');
        $('#LoginWithAmazon').click(openLoginModal);
    }

    const manageAmazonLoginResponse = (response) => {
        if (response.error) {
            alert('oauth error ' + response.error);
            return;
        }
        amazon.Login.retrieveProfile(response.access_token, getAmazonUserProfile);
    }

    const getAmazonUserProfile = (response) => {
        alert('Hello, ' + response.profile.Name);
        alert('Your e-mail address is ' + response.profile.PrimaryEmail);
        alert('Your unique ID is ' + response.profile.CustomerId);
        if (window.console && window.console.log)
            window.console.log(response);
    }

    const openLoginModal = () => {
        let options = { scope: 'profile' };
        amazon.Login.authorize(options, manageAmazonLoginResponse);
        return false;
    };

    const addAmazonScripts = () => {
        let script = document.createElement('script');

        script.id = 'amazon-login-sdk';
        script.type = 'text/javascript';
        script.async = true;
        script.src = 'https://api-cdn.amazon.com/sdk/login1.js';

        document.getElementById('amazon-root').appendChild(script);        
    };

    $('document').ready(() => {
        addAmazonScripts();
        window.onAmazonLoginReady = registerAmazonClientId;
    });
})();