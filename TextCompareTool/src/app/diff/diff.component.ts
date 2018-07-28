import {
  Component,
  ViewEncapsulation,
  Input,
  Output,
  OnInit
} from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { DiffEditorModel } from 'ngx-monaco-editor';

@Component({
  selector: 'diff',
  styleUrls: ["./diff.component.css"],
  templateUrl: "./diff.component.html",
  encapsulation: ViewEncapsulation.None,
})
export class DiffComponent implements OnInit {

  text1 = "text1-aaa";
  text2 = "text2-bbb";
  
  @Output()
  selectedLang = "java";
  @Output()
  selectedTheme = "vs";

  @Input()
  languages = [
    "bat",
    "c",
    "coffeescript",
    "cpp",
    "csharp",
    "csp",
    "css",
    "dockerfile",
    "fsharp",
    "go",
    "handlebars",
    "html",
    "ini",
    "java",
    "javascript",
    "json",
    "less",
    "lua",
    "markdown",
    "msdax",
    "mysql",
    "objective-c",
    "pgsql",
    "php",
    "plaintext",
    "postiats",
    "powershell",
    "pug",
    "python",
    "r",
    "razor",
    "redis",
    "redshift",
    "ruby",
    "rust",
    "sb",
    "scss",
    "sol",
    "sql",
    "st",
    "swift",
    "typescript",
    "vb",
    "xml",
    "yaml"
  ];

  @Input()
  themes = [
    {
      value: "vs",
      name: "Visual Studio"
    },
    {
      value: "vs-dark",
      name: "Visual Studio Dark"
    },
    {
      value: "hc-black",
      name: "High Contrast Dark"
    }
  ];

  // input
  inputOptions = { theme: "vs", language: 'text/plain' };
  // compare, output
  diffOptions = { theme: "vs", language: "xml", renderSideBySide: true };
  originalModel: DiffEditorModel = {
    code: 'heLLo world!',
    language: 'text/plain' 
  };

  modifiedModel: DiffEditorModel = {
    code: 'hello orlando!',
    language: 'text/plain' 
  };

  public localState: any;
  constructor(
    public route: ActivatedRoute
  ) {}

  public ngOnInit() {
    
  }

  onChangeLanguage(language) {
    this.inputOptions = Object.assign({}, this.inputOptions, { language: language });
    this.diffOptions = Object.assign({}, this.diffOptions, { language: language });
  }
  onChangeTheme(theme) {
    this.inputOptions = Object.assign({}, this.inputOptions, { theme: theme });
    this.diffOptions = Object.assign({}, this.diffOptions, { theme: theme });
  }

  onChangeInline(checked) {
    this.diffOptions = Object.assign({}, this.diffOptions, { renderSideBySide: !checked });
  }

  onCompare() {
    this.originalModel = Object.assign({}, this.originalModel, { code: this.text1 });
    this.modifiedModel = Object.assign({}, this.originalModel, { code: this.text2 });
    window.scrollTo(0, 0);
  }
}
