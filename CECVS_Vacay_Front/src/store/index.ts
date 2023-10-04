import { createStore } from 'vuex'
import state from '@/store/state'
import actions from '@/store/actions'
import mutations from '@/store/mutations'
import getters from '@/store/getters'

const store = createStore({
    state,
    actions,
    mutations,
    getters
});

export default store; 