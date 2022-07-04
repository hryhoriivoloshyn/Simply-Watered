export default class AuthLocalStorage {
  static STORAGE_KEY = "token";

  static getAccessToken() {
    return window.localStorage.getItem(AuthLocalStorage.STORAGE_KEY);
  }

  static setAccessToken(token) {
    window.localStorage.setItem(AuthLocalStorage.STORAGE_KEY, token);
  }

  static removeAccessToken() {
    window.localStorage.removeItem(AuthLocalStorage.STORAGE_KEY);
  }
}
