export function authHeader() {
  // return authorization header with basic auth credentials
  const user = JSON.parse(<string>localStorage.getItem('user'))

  if (user && user.token) {
    return { Authorization: 'Bearer ' + user.token }
  } else {
    return {}
  }
}
