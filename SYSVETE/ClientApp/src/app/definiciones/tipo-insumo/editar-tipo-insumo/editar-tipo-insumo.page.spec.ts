import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { EditarTipoInsumoPage } from './editar-tipo-insumo.page';

describe('EditarTipoInsumoPage', () => {
  let component: EditarTipoInsumoPage;
  let fixture: ComponentFixture<EditarTipoInsumoPage>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ EditarTipoInsumoPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(EditarTipoInsumoPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
