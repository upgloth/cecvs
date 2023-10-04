import { apiV1 } from '@/apis'
import { IFuncionarioDTO } from '@/models/funcionario';

async function getFuncionario(idFuncionario: number)
    : Promise<IFuncionarioDTO> {
    const { data } = await apiV1.get(
        `funcionarios/${idFuncionario}`
    );

    return data;
}

async function getFuncionarios()
    : Promise<IFuncionarioDTO[]> {

    const { data } = await apiV1.get(
        `funcionarios`
    );

    return data;
};

async function postFuncionario(funcionario: IFuncionarioDTO)
    : Promise<boolean> {

    const response = await apiV1.post(
        `funcionarios`, funcionario
    );

    return response?.status == 201;
}

async function putFuncionario(funcionario: IFuncionarioDTO)
    : Promise<boolean> {

    const response = await apiV1.put(
        `funcionarios/${funcionario.idFuncionario}`, funcionario
    );

    return response?.status == 200;
}

async function deleteFuncionario(idFuncionario: number)
    : Promise<boolean> {

    const response = await apiV1.delete(
        `funcionarios/${idFuncionario}`
    );

    return response?.status == 204;
}

export {
    getFuncionario,
    getFuncionarios,
    postFuncionario,
    putFuncionario,
    deleteFuncionario
};