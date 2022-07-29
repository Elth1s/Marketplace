import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import { StyledEngineProvider } from '@mui/material/styles';


import App from './App';
import { store } from './store'
import { AuthUser } from './pages/auth/actions';
import { getLocalAccessToken } from './http_comon';
import reportWebVitals from './reportWebVitals';

import './styles/index.css';



let token = getLocalAccessToken();
if (token) {
  AuthUser(token, store.dispatch);
}

ReactDOM.render(
  <Provider store={store}>
    <BrowserRouter>
      <StyledEngineProvider injectFirst>
        <App />
      </StyledEngineProvider>
    </BrowserRouter>
  </Provider>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
