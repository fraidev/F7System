export function authHeader() {
  // return authorization header with basic auth credentials

  const userCache = localStorage.getItem('user')
  let user
  if (userCache) {
    user = JSON.parse(localStorage.getItem('user'))
  }
  if (user && user?.token) {
    return { Authorization: 'Bearer ' + user.token }
  } else {
    return {}
  }
}
