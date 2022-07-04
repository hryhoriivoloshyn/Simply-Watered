import AuthLocalStorage from "./AuthLocalStorage";

export default class AuthorizeApi {
  static isSignedIn() {
    return !!AuthLocalStorage.getToken();
  }

  static login = async (data) => {
    const response = await fetch(`api/auth/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    })
      .then((response) => {
        if (response.token !== null) {
          AuthLocalStorage.setAccessToken(response.token);
        }
      })
      .catch(async (error) => {
        switch (error.response.status) {
          case 400:
            console.log("error", "Щось пішло не так");
            break;
        }
      });
    return response;
  };

  static register = async (data) => {
    const response = await fetch(`api/auth/register`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    }).catch((error) => {
      if (error.response.status === 400) {
        console.log("error", "Щось пішло не так");
      }
    });
    return response;
  };
}
