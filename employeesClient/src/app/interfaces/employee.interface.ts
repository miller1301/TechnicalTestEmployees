export interface IEmployee {
    id: number;
    cedula: number;
    nombre: string;
    foto: string;
    fechaIngreso: string;
    jobTitleId: number;
    jobTitleDTO: IJobTitle;
}

interface IJobTitle {
    id: number;
    nombre: string;
}