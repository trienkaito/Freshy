import { combineReducers } from 'redux';

import productsReducer from '../product/ProductReducer.js';
import UserReducer from '../user/UserReducer.js';

const rootReducer = combineReducers({
     cart : productsReducer, user : UserReducer
});

export default rootReducer;