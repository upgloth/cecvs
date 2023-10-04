
interface IFeriasDTO {
    idFerias: number,
    idFuncionario: number,
    dtInicio: string,
    qtDias: number
}

export type { IFeriasDTO };

interface IFeriasUpdateDTO {
    idFerias: number,
    dtInicio: string,
    qtDias: number
}

export type { IFeriasUpdateDTO };


interface IFeriasExpandidoDTO {
    idFerias: number,
    idFuncionario: number,
    coMatricula: string,
    noFuncionario: string,
    dtInicio: string,
    qtDias: number
}

export type { IFeriasExpandidoDTO };