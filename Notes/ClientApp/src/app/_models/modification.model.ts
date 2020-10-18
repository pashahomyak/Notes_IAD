export class ModificationModel {
  constructor(
    public token: string,
    public oldValue: string,
    public newValue: string
  ) {}
}
