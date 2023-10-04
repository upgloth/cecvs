import { SET_DEPARTAMENTOS } from '@/store/mutation-types'
import { IDepartamentoDTO } from '@/models/departamento';

export default ({
    async [SET_DEPARTAMENTOS]({ commit }, data: IDepartamentoDTO[]) {
        commit(SET_DEPARTAMENTOS, data);
    },
});