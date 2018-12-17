# Altairis.Services.LoginApprovals

This library allows passwordless login to web sites and uses another device to approve given login.

This login method may be quicker and also it's more secure on partially trusted computers. They're secure enough to use a service, but not to type the password.

Big identity providers like Microsoft, Google or Facebook are using similar methods using their authenticator apps. For a smaller project is not practical to write and deploy their custom apps, but the behavior can be done with standard web browser.

## Login flow

From the user's perspective the login process is as follows:

1. User will go to login page and chooses to sign in using another device.
2. The site displays request ID (ie. **B1E2-74D5**) and a QR code.
3. User opens the URL encoded in QR code on a mobile device, where he's already signed in (or will sign in) and verifies the request ID.
4. User confirms the login.
5. The original page is refreshing constantly and once the request is approved on mobile device, the user is logged in.

## Security risks

* The user may be tricked into confirming attacker's login with social engineering. In such case is user an idiot and deserves what happens.
* The login page may be spoofed and the QR code may lead to attacker's site to trick the user to enter their credentials. Usual precautions apply for entering credentials.

## Contributor Code of Conduct

This project adheres to No Code of Conduct. We are all adults. We accept anyone's contributions. Nothing else matters.

For more information please visit the [No Code of Conduct](https://github.com/domgetter/NCoC) homepage.